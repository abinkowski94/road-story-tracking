using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebJob.Images.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RemoveImage(string path)
        {
            var removeTask = Task.Run(async () =>
            {
                var blobsConfigurationSection = _configuration.GetSection("Storage:Blobs");
                if (blobsConfigurationSection == null)
                {
                    throw new ApplicationException("Could not find 'Storage:Blobs' configuration section");
                }
                var blobsConfigurationSectionChildren = blobsConfigurationSection.GetChildren();

                foreach (var item in blobsConfigurationSectionChildren)
                {
                    var connectionString = item["ConnectionString"]
                    ?? throw new ApplicationException("Could not find 'ConnectionString' configuration section");

                    // TODO: delete other sizes
                    var cloudBlockBlob = new CloudBlockBlob(new Uri(path), GetBlobClient(connectionString));
                    await cloudBlockBlob.DeleteIfExistsAsync();
                }
            });

            removeTask.Wait();
        }

        public void ResizeImage(ResizeImageMessage resizeImageMessage)
        {
            // TODO: resizing
        }

        private CloudBlobClient GetBlobClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }

        private async Task<CloudBlobContainer> GetDefaultContainer(string connectionString, string defaultContainerName)
        {
            var client = GetBlobClient(connectionString);
            var container = client.GetContainerReference(defaultContainerName);

            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            return container;
        }
    }
}