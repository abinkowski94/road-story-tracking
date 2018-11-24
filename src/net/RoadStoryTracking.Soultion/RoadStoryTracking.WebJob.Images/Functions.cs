using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using RoadStoryTracking.WebJob.Images.Converters;

namespace RoadStoryTracking.WebJob.Images
{
    [StorageAccount("Storage:ImageQueue:ConnectionString")]
    public class Functions
    {
        private readonly JsonSerializerSettings _jsonSerializationSettings;

        public Functions()
        {
            _jsonSerializationSettings = new JsonSerializerSettings { Converters = { new ImageMessageConverter() } };
        }

        public void ProcessWorkItem([QueueTrigger("images-queue")] string imageMessageString, ILogger logger)
        {
            var imageMessage = JsonConvert.DeserializeObject<ImageMessage>(imageMessageString, _jsonSerializationSettings);
            if (imageMessage != null)
            {
            }
        }
    }
}