using AutoMapper;
using RoadStoryTracking.Model.Responses;
using BMR = RoadStoryTracking.WebApi.Business.BusinessModels.Responses;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class TechnicalProfile : Profile
    {
        public TechnicalProfile()
        {
            CreateMap(typeof(BMR.BaseResponse), typeof(BaseResponse));
            CreateMap(typeof(BMR.SuccessResponse<>), typeof(SuccessResponse<>));
            CreateMap(typeof(BMR.ErrorResponse), typeof(ErrorResponse));
        }
    }
}