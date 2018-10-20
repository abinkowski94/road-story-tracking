using AutoMapper;
using RoadStoryTracking.Model.Models.Contact;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Business.Models.Contact.Contact, Contact>();
            CreateMap<Business.Models.Contact.Inviation, Inviation>();
        }
    }
}