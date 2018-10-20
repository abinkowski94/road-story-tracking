namespace RoadStoryTracking.WebApi.Business.Models.Responses
{
    public class SuccessResponse<T> : BaseResponse
    {
        public T Result { get; private set; }

        public SuccessResponse(T result) : base()
        {
            Result = result;
        }
    }
}