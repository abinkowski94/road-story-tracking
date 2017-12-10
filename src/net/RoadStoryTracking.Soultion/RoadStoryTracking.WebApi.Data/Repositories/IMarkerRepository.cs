using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface IMarkerRepository : IDisposable
    {
        Marker AddMarker(Marker marker);

        Marker GetMarker(Guid markerId);

        List<Marker> GetMarkers();

        List<Marker> GetUsersMarkers(string userId);

        Marker DeleteMarker(Marker marker);
    }
}