namespace RoadStoryTracking.WebApi.Business.ImageService
{
    public interface IImageService
    {
        bool DeleteImage(string path);

        string SaveImage(string base64Image, string imageName, string location);
    }
}