namespace RoadStoryTracking.WebApi.Business.BusinessModels.Responses
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