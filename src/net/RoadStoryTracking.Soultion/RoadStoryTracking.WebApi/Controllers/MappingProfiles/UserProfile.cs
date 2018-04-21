using AutoMapper;
using RoadStoryTracking.Model.Models.User;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Business.Models.User.TokenInfo, TokenInfo>();

            CreateMap<Business.Models.User.ApplicationUser, ApplicationUser>();

            CreateMap<ApplicationUser, Business.Models.User.ApplicationUser>()
                .ForMember(p => p.UserName, p => p.MapFrom(d => d.Email))
                .ForMember(p => p.Id, p => p.Ignore());
        }
    }
}