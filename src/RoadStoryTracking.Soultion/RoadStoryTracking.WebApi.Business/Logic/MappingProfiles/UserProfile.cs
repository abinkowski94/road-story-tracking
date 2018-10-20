using AutoMapper;
using RoadStoryTracking.WebApi.Business.Models.User;

namespace RoadStoryTracking.WebApi.Business.Logic.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, Data.Models.ApplicationUser>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Data.Models.ApplicationUser, ApplicationUser>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}