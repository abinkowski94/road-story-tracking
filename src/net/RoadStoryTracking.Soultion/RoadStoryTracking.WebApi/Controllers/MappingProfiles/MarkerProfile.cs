using AutoMapper;
using RoadStoryTracking.Model.Models.Marker;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class MarkerProfile : Profile
    {
        public MarkerProfile()
        {
            CreateMap<Business.Models.Marker.Marker, Marker>().ReverseMap();
            CreateMap<Business.Models.Marker.MarkerOwner, MarkerOwner>().ReverseMap();
            CreateMap<Business.Models.Marker.MarkerInvitation, MarkerInvitation>().ReverseMap();
            CreateMap<Business.Models.Marker.IncomingMarkerInviation, IncomingMarkerInviation>().ReverseMap();
        }
    }
}