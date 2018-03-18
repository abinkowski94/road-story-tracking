using AutoMapper;
using RoadStoryTracking.Model.Models.Marker;
using BMM = RoadStoryTracking.WebApi.Business.BusinessModels.Marker;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class MarkerProfile : Profile
    {
        public MarkerProfile()
        {
            CreateMap<BMM.Marker, Marker>().ReverseMap();
            CreateMap<BMM.MarkerOwner, MarkerOwner>().ReverseMap();
        }
    }
}