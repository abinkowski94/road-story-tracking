using Newtonsoft.Json;

namespace RoadStoryTracking.WebApi.Business.Models.Messages
{
    public abstract class ImageMessage
    {
        [JsonProperty("messageType")]
        public virtual ImageMessageTypes MessageType => ImageMessageTypes.None;
    }
}