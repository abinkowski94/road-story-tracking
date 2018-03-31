using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.ImageService
{
    public interface IImageService
    {
        Task<bool> DeleteImageAsync(string path);

        Task<string> SaveImageAsync(string base64Image, string imageName, string location);
    }
}