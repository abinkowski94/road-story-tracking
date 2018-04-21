using System;

namespace RoadStoryTracking.WebApi.Business.Models.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public Exception Exception { get; private set; }

        public ErrorResponse(Exception exception) : base()
        {
            Exception = exception;
        }
    }
}