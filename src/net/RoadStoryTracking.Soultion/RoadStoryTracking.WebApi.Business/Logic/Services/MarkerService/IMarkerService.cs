using System;
using RoadStoryTracking.WebApi.Business.Models.Marker;
using RoadStoryTracking.WebApi.Business.Models.Responses;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.MarkerService
{
    public interface IMarkerService
    {
        BaseResponse AddMarker(Marker marker, string userId);

        BaseResponse DeleteMarker(Guid markerId, string userId);

        BaseResponse GetMarker(Guid markerId);

        BaseResponse GetMarkers();

        BaseResponse GetUsersMarkers(string userId);

        BaseResponse UpdateMarker(Marker marker, string userId);
    }
}