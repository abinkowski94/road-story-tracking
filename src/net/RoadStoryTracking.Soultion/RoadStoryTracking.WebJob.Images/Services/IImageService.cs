using Microsoft.Extensions.Logging;
using RoadStoryTracking.WebApi.Business.Models.Messages;

namespace RoadStoryTracking.WebJob.Images.Services
{
    public interface IImageService
    {
        void RemoveImage(string path, ILogger logger);

        void ResizeImage(ResizeImageMessage resizeImageMessage, ILogger logger);
    }
}