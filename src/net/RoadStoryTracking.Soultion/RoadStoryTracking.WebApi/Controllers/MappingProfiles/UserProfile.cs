using AutoMapper;
using RoadStoryTracking.Model.Models.User;
using BMU = RoadStoryTracking.WebApi.Business.BusinessModels.User;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BMU.TokenInfo, TokenInfo>();

            CreateMap<BMU.ApplicationUser, ApplicationUser>();

            CreateMap<ApplicationUser, BMU.ApplicationUser>()
                .ForMember(p => p.UserName, p => p.MapFrom(d => d.Email))
                .ForMember(p => p.Id, p => p.Ignore());
        }
    }
}