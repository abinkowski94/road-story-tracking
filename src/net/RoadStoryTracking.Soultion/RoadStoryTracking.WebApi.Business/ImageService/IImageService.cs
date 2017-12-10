namespace RoadStoryTracking.WebApi.Business.ImageService
{
    public interface IImageService
    {
        string SaveImage(string base64Image, string imageName, string location);

        bool DeleteImage(string path);
    }
}
