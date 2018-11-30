using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using RoadStoryTracking.WebJob.Images.Converters;
using RoadStoryTracking.WebJob.Images.Services;

namespace RoadStoryTracking.WebJob.Images
{
    [StorageAccount("Storage:DefaultQueue:ConnectionString")]
    public class Functions
    {
        private readonly IImageService _imageService;
        private readonly JsonSerializerSettings _jsonSerializationSettings;

        public Functions(IImageService imageService)
        {
            _imageService = imageService;
            _jsonSerializationSettings = new JsonSerializerSettings { Converters = { new ImageMessageConverter() } };
        }

        public void ProcessWorkItem([QueueTrigger("images-queue")] string imageMessageString, ILogger logger)
        {
            var imageMessage = JsonConvert.DeserializeObject<ImageMessage>(imageMessageString, _jsonSerializationSettings);
            if (imageMessage != null)
            {
                switch (imageMessage)
                {
                    case DeleteImageMessage deleteImageMessage:
                        _imageService.RemoveImage(deleteImageMessage.FullBlobPath, logger);
                        break;

                    case ResizeImageMessage resizeImageMessage:
                        _imageService.ResizeImage(resizeImageMessage, logger);
                        break;
                }
            }
        }
    }
}