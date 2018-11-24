using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using System;

namespace RoadStoryTracking.WebJob.Images.Converters
{
    public class ImageMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ImageMessage);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jObject = JObject.Load(reader);
                var messageType = (ImageMessageTypes)(jObject["messageType"]?.Value<int>() ?? 0);

                switch (messageType)
                {
                    case ImageMessageTypes.None:
                        return null;

                    case ImageMessageTypes.Resize:
                        return jObject.ToObject<ResizeImageMessage>(serializer);

                    case ImageMessageTypes.Delete:
                        return jObject.ToObject<DeleteImageMessage>(serializer);

                    default:
                        return null;
                }
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}