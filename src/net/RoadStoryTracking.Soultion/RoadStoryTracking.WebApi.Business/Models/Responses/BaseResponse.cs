using System;

namespace RoadStoryTracking.WebApi.Business.Models.Responses
{
    public abstract class BaseResponse
    {
        public Guid Id { get; private set; }

        protected BaseResponse()
        {
            Id = Guid.NewGuid();
        }
    }
}