using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ImageService
{
    public interface IImageService
    {
        void DeleteImage(string path);

        Task<string> SaveImageAsync(string base64Image, string imageName);
    }
}