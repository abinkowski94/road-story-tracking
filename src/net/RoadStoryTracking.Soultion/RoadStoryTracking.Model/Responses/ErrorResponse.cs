using Newtonsoft.Json;
using System;
using System.Net;

namespace RoadStoryTracking.Model.Responses
{
    public class ErrorResponse : BaseResponse
    {
        [JsonProperty("exception")]
        public Exception Exception { get; private set; }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public ErrorResponse(Exception exception) : base()
        {
            Exception = exception;
        }
    }
}