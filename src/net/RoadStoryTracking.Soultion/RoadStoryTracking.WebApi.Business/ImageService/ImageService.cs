using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.ImageService
{
    public class ImageService : IImageService
    {
        private readonly string _defaultContrainerName;
        private readonly string _storageConnectionString;

        public ImageService(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["Storage:DefaultContainerName"])
                || string.IsNullOrEmpty(configuration["Storage:ConnectionString"]))
            {
                throw new ApplicationException("Image service has bad configuration. Missing configuration keys");
            }

            _defaultContrainerName = configuration["Storage:DefaultContainerName"];
            _storageConnectionString = configuration["Storage:ConnectionString"];
        }

        public Task<bool> DeleteImageAsync(string path)
        {
            return Task.Run(async () =>
            {
                var cloudBlockBlob = new CloudBlockBlob(new Uri(path), GetBlobClient());
                return await cloudBlockBlob.DeleteIfExistsAsync();
            });
        }

        public Task<string> SaveImageAsync(string base64Image, string imageName, string location)
        {
            return Task.Run(async () =>
            {
                base64Image = ClearBase64Fromat(base64Image);

                if (TryGetFromBase64String(base64Image, out byte[] bytes))
                {
                    var imageFullPath = $"assets\\{location}\\{imageName}.jpg";
                    var container = await GetDefaultContainer();
                    var cloudBlockBlob = container.GetBlockBlobReference(imageFullPath);
                    await cloudBlockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
                    cloudBlockBlob.Properties.ContentType = "image/jpeg";

                    return cloudBlockBlob.Uri.ToString();
                }

                return null;
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
            var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }

        private async Task<CloudBlobContainer> GetDefaultContainer()
        {
            var client = GetBlobClient();
            var container = client.GetContainerReference(_defaultContrainerName);

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