using System.Net;

namespace RoadStoryTracking.Model.Responses
{
    public class SuccessResponse<T> : BaseResponse
    {
        public T Result { get; private set; }

        public override HttpStatusCode StatusCode => HttpStatusCode.OK;

        public SuccessResponse(T result) : base()
        {
            Result = result;
        }
    }
}