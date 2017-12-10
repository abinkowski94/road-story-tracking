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

        public string SaveImage(string base64Image, string imageName, string location)
        {
            base64Image = ClearBase64Fromat(base64Image);

            var pathWithFolderName = Path.Combine(_enviroment.WebRootPath, $"assets\\{location}");
            var imagePath = $"{pathWithFolderName}\\{imageName}.jpg";

            if (!Directory.Exists(pathWithFolderName))
            {
                var directory = Directory.CreateDirectory(pathWithFolderName);
            }

            var bytes = Convert.FromBase64String(base64Image);
            File.Create(imagePath).Dispose();

            using (var writer = new BinaryWriter(File.OpenWrite(imagePath)))
            {
                writer.Write(bytes);
                writer.Flush();
            }

            return $"assets/{location}/{imageName}.jpg";
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

        private string ClearBase64Fromat(string base64Image)
        {
            var indexOfFormatEnd = base64Image.IndexOf(',') + 1;
            var formatString = base64Image.Substring(0, indexOfFormatEnd);
            return base64Image.Replace(formatString, "");
        }
    }
}
