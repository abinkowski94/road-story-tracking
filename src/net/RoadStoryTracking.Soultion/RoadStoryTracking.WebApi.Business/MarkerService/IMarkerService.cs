using RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System;

namespace RoadStoryTracking.WebApi.Business.MarkerService
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