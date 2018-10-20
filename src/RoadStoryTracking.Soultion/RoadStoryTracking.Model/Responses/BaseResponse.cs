using Newtonsoft.Json;
using System;
using System.Net;

namespace RoadStoryTracking.Model.Responses
{
    public abstract class BaseResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonIgnore]
        public abstract HttpStatusCode StatusCode { get; }

        protected BaseResponse()
        {
            Id = Guid.NewGuid();
        }
    }
}