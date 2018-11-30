using Newtonsoft.Json;
using System;
using System.Net;

namespace RoadStoryTracking.Model.Responses
{
    public class ErrorResponse : BaseResponse
    {
#if DEBUG

        [JsonProperty("exception")]
        public Exception Exception { get; private set; }

#else
        [JsonProperty("exception")]
        public string Exception { get; private set; }
#endif

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public ErrorResponse(Exception exception) : base()
        {
#if DEBUG
            Exception = exception;
#else
            Exception = exception.Message
#endif
        }
    }
}