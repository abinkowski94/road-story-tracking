using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Responses;
using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Controllers;
using BMR = RoadStoryTracking.WebApi.Business.BusinessModels.Responses;

namespace RoadStoryTracking.WebApi.Extensions
{
    public static class ResponseExtensions
    {
        public static IActionResult GetActionResult<TSource, TDestination>(this BMR.BaseResponse inputResponse, BaseController controller)
            where TSource : class
            where TDestination : class
        {
            BaseResponse outputResponse;

            if (inputResponse is BMR.ErrorResponse)
            {
                outputResponse = controller.LocalMapper.Map<ErrorResponse>(inputResponse);
            }
            else if (inputResponse is BMR.SuccessResponse<TSource>)
            {
                outputResponse = controller.LocalMapper.Map<SuccessResponse<TDestination>>(inputResponse);
            }
            else
            {
                throw new CustomApplicationException("The provided response is not supported");
            }

            return new ObjectResult(outputResponse)
            {
                StatusCode = (int)outputResponse.StatusCode
            };
        }

        public static IActionResult GetActionResult(this BMR.BaseResponse inputResponse, BaseController controller)
        {
            BaseResponse outputResponse;

            if (inputResponse is BMR.ErrorResponse)
            {
                outputResponse = controller.LocalMapper.Map<ErrorResponse>(inputResponse);
            }
            else if (inputResponse is BMR.SuccessResponse<object>)
            {
                outputResponse = controller.LocalMapper.Map<SuccessResponse<object>>(inputResponse);
            }
            else
            {
                throw new CustomApplicationException("The provided response is not supported");
            }

            return new ObjectResult(outputResponse)
            {
                StatusCode = (int)outputResponse.StatusCode
            };
        }
    }
}