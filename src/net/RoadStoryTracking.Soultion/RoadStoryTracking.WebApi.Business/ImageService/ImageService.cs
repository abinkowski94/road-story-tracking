using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace RoadStoryTracking.WebApi.Business.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _enviroment;

        public ImageService(IHostingEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        public bool DeleteImage(string path)
        {
            var fullPath = Path.Combine(_enviroment.WebRootPath, path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }

        public string SaveImage(string base64Image, string imageName, string location)
        {
            base64Image = ClearBase64Fromat(base64Image);

            var pathWithFolderName = Path.Combine(_enviroment.WebRootPath, $"assets\\{location}");
            var imagePath = $"{pathWithFolderName}\\{imageName}.jpg";

            if (!Directory.Exists(pathWithFolderName))
            {
                var directory = Directory.CreateDirectory(pathWithFolderName);
            }

            if (TryGetFromBase64String(base64Image, out byte[] bytes))
            {
                File.Create(imagePath).Dispose();

                using (var writer = new BinaryWriter(File.OpenWrite(imagePath)))
                {
                    writer.Write(bytes);
                    writer.Flush();
                }

                return $"assets/{location}/{imageName}.jpg";
            }

            return null;
        }

        private string ClearBase64Fromat(string base64Image)
        {
            var indexOfFormatEnd = base64Image.IndexOf(',') + 1;
            var formatString = base64Image.Substring(0, indexOfFormatEnd);
            return base64Image.Replace(formatString, "");
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