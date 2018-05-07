using AutoMapper;
using RoadStoryTracking.WebApi.Business.Models.Contact;

namespace RoadStoryTracking.WebApi.Business.Logic.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Data.Models.ApplicationUser, Contact>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Data.Models.Contact, Inviation>()
                .ForMember(dst => dst.SendDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.RequestedBy))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}