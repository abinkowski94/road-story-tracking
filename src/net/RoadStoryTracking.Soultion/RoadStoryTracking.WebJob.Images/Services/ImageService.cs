using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebJob.Images.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly Size _largeSize;
        private readonly Size _mediumSize;
        private readonly Size _smallSize;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _largeSize = new Size(int.Parse(_configuration["Images:Large:Width"] ?? "1920"), int.Parse(_configuration["Images:Large:Height"] ?? "1080"));
            _mediumSize = new Size(int.Parse(_configuration["Images:Medium:Width"] ?? "1366"), int.Parse(_configuration["Images:Medium:Height"] ?? "768"));
            _smallSize = new Size(int.Parse(_configuration["Images:Small:Width"] ?? "320"), int.Parse(_configuration["Images:Small:Height"] ?? "180"));
        }

        public void RemoveImage(string path, ILogger logger)
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

                    var cloudBlockBlob = new CloudBlockBlob(new Uri(path), GetBlobClient(connectionString));
                    await cloudBlockBlob.DeleteIfExistsAsync();
                }
            });

            removeTask.Wait();
        }

        public void ResizeImage(ResizeImageMessage resizeImageMessage, ILogger logger)
        {
            var resizeTask = Task.Run(async () =>
            {
                try
                {
                    var connectionString = _configuration[$"Storage:Blobs:{resizeImageMessage.BlobStorageName}:ConnectionString"]
                        ?? throw new ApplicationException($"Could not find 'Storage:Blobs:{resizeImageMessage.BlobStorageName}:ConnectionString' configuration section");
                    var cloudBlockBlob = new CloudBlockBlob(new Uri(resizeImageMessage.FullBlobPath), GetBlobClient(connectionString));

                    if (await cloudBlockBlob.ExistsAsync())
                    {
                        using (var stream = new MemoryStream())
                        {
                            await cloudBlockBlob.DownloadToStreamAsync(stream);
                            using (var bitmap = new Bitmap(stream))
                            {
                                var newSize = resizeImageMessage.Size == ImageSize.L ? _largeSize : resizeImageMessage.Size == ImageSize.M ? _mediumSize : _smallSize;
                                using (var resizedImage = FixedSize(bitmap, newSize))
                                {
                                    var imageFullPath = $"{resizeImageMessage.Location}\\{resizeImageMessage.OriginalName}-{resizeImageMessage.Size}.jpg";
                                    var resizedImageBlob = cloudBlockBlob.Container.GetBlockBlobReference(imageFullPath);
                                    var bytes = ImageToByteArray(resizedImage);
                                    await resizedImageBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
                                    resizedImageBlob.Properties.ContentType = "image/jpeg";
                                    await resizedImageBlob.SetPropertiesAsync();
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                }
            });

            resizeTask.Wait();
        }

        private static Image FixedSize(Image imgPhoto, Size size)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            var sourceX = 0;
            var sourceY = 0;
            var destX = 0;
            var destY = 0;

            var nPercent = 0f;
            var nPercentW = 0f;
            var nPercentH = 0f;

            nPercentW = size.Width / (float)sourceWidth;
            nPercentH = size.Height / (float)sourceHeight;

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((size.Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((size.Height - (sourceHeight * nPercent)) / 2);
            }

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            using (var grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.Clear(Color.White);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            }

            return bmPhoto;
        }

        private static byte[] ImageToByteArray(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        private static Size ResizeKeepAspect(Size sourceSize, Size newSize)
        {
            var enlarge = newSize.Width > sourceSize.Width || newSize.Height > sourceSize.Height;

            newSize.Width = enlarge ? newSize.Width : Math.Min(newSize.Width, sourceSize.Width);
            newSize.Height = enlarge ? newSize.Height : Math.Min(newSize.Height, sourceSize.Height);

            var rnd = Math.Min(newSize.Width / (decimal)sourceSize.Width, newSize.Height / (decimal)sourceSize.Height);

            return new Size((int)Math.Round(sourceSize.Width * rnd), (int)Math.Round(sourceSize.Height * rnd));
        }

        private CloudBlobClient GetBlobClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }
    }
}