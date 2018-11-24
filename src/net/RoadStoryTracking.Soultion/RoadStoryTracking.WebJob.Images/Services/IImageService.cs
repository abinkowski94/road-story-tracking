using RoadStoryTracking.WebApi.Business.Models.Messages;

namespace RoadStoryTracking.WebJob.Images.Services
{
    public interface IImageService
    {
        void RemoveImage(string path);

        void ResizeImage(ResizeImageMessage resizeImageMessage);
    }
}