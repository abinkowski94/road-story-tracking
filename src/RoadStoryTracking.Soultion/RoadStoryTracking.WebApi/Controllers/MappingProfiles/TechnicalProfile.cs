using AutoMapper;
using RoadStoryTracking.Model.Responses;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class TechnicalProfile : Profile
    {
        public TechnicalProfile()
        {
            CreateMap(typeof(Business.Models.Responses.BaseResponse), typeof(BaseResponse));
            CreateMap(typeof(Business.Models.Responses.SuccessResponse<>), typeof(SuccessResponse<>));
            CreateMap(typeof(Business.Models.Responses.ErrorResponse), typeof(ErrorResponse));
        }
    }
}