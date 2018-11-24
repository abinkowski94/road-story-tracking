using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RoadStoryTracking.WebApi.Business.Logic.Services.Messaging;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly string _imageBlobStorageConnectionString;
        private readonly string _imageBlobStorageDefaultContrainerName;
        private readonly string _imageBlobStorageLocation;
        private readonly string _imageBlobStorageName;
        private readonly IMessagingService _messagingService;

        public ImageService(IMessagingService messagingService, IConfiguration configuration)
        {
            _messagingService = messagingService;

            _imageBlobStorageName = configuration["Storage:ImageBlobStorageName"]
                ?? throw new ApplicationException("The key 'Storage:ImageBlobStorageName' is not registered");

            _imageBlobStorageConnectionString = configuration[$"Storage:Blobs:{_imageBlobStorageName}:ConnectionString"]
                ?? throw new ApplicationException($"The key 'Storage:Blobs:{_imageBlobStorageName}:ConnectionString' is not registered");

            _imageBlobStorageDefaultContrainerName = configuration[$"Storage:Blobs:{_imageBlobStorageName}:DefaultContainerName"]
                ?? throw new ApplicationException($"The key 'Storage:Blobs:{_imageBlobStorageName}:DefaultContainerName' is not registered");

            _imageBlobStorageLocation = configuration[$"Storage:Blobs:{_imageBlobStorageName}:Location"]
                ?? throw new ApplicationException($"The key 'Storage:Blobs:{_imageBlobStorageName}:Location' is not registered");
        }

        public void DeleteImage(string path)
        {
            _messagingService.PutImageMessageToQueue(new DeleteImageMessage
            {
                FullBlobPath = path
            });
        }

        public Task<string> SaveImageAsync(string base64Image, string imageName)
        {
            return Task.Run(async () =>
            {
                base64Image = ClearBase64Fromat(base64Image);
                if (!TryGetFromBase64String(base64Image, out var bytes))
                {
                    return null;
                }

                var imageFullPath = $"{_imageBlobStorageLocation}\\{imageName}.jpg";
                var container = await GetDefaultContainer();
                var cloudBlockBlob = container.GetBlockBlobReference(imageFullPath);
                await cloudBlockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
                cloudBlockBlob.Properties.ContentType = "image/jpeg";
                await cloudBlockBlob.SetPropertiesAsync();

                foreach (var imageSize in (ImageSize[])Enum.GetValues(typeof(ImageSize)))
                {
                    _messagingService.PutImageMessageToQueue(new ResizeImageMessage
                    {
                        FullBlobPath = cloudBlockBlob.Uri.ToString(),
                        BlobStorageName = _imageBlobStorageName,
                        OriginalName = imageName,
                        Size = imageSize
                    });
                }

                return cloudBlockBlob.Uri.ToString();
            });
        }

        private string ClearBase64Fromat(string base64Image)
        {
            var indexOfFormatEnd = base64Image.IndexOf(',') + 1;
            var formatString = base64Image.Substring(0, indexOfFormatEnd);

            return base64Image.Replace(formatString, "");
        }

        private CloudBlobClient GetBlobClient()
        {
            var storageAccount = CloudStorageAccount.Parse(_imageBlobStorageConnectionString);
            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }

        private async Task<CloudBlobContainer> GetDefaultContainer()
        {
            var client = GetBlobClient();
            var container = client.GetContainerReference(_imageBlobStorageDefaultContrainerName);

            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            return container;
        }

        private bool TryGetFromBase64String(string input, out byte[] output)
        {
            output = null;
            try
            {
                output = Convert.FromBase64String(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}