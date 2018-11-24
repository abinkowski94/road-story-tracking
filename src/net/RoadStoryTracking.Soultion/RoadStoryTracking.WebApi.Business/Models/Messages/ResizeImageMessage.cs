using Newtonsoft.Json;

namespace RoadStoryTracking.WebApi.Business.Models.Messages
{
    public class ResizeImageMessage : ImageMessage
    {
        [JsonProperty("blobStorageName")]
        public string BlobStorageName { get; set; }

        [JsonProperty("fullBlobPath")]
        public string FullBlobPath { get; set; }

        [JsonProperty("messageType")]
        public override ImageMessageTypes MessageType => ImageMessageTypes.Resize;

        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        [JsonProperty("size")]
        public ImageSize Size { get; set; }
    }
}