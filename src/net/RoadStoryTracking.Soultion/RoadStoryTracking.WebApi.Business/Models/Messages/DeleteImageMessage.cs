using Newtonsoft.Json;

namespace RoadStoryTracking.WebApi.Business.Models.Messages
{
    public class DeleteImageMessage : ImageMessage
    {
        [JsonProperty("fullBlobPath")]
        public string FullBlobPath { get; set; }

        [JsonProperty("messageType")]
        public override ImageMessageTypes MessageType => ImageMessageTypes.Delete;
    }
}