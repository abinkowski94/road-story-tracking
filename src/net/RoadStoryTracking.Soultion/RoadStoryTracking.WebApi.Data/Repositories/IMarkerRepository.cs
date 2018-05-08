using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface IMarkerRepository : IDisposable
    {
        Marker AddMarker(Marker marker);

        List<MarkerInvitation> AddMarkerInvitations(List<MarkerInvitation> markerInvitations);

        Marker DeleteMarker(Marker marker);

        List<MarkerImage> DeleteMarkerImages(List<MarkerImage> markerImages);

        List<MarkerInvitation> DeleteMarkerInvitations(List<MarkerInvitation> markerInvitations);

        Marker GetMarker(Guid markerId);

        List<Marker> GetMarkers();

        Dictionary<string, ApplicationUser> GetUsersDictionary(List<string> userIds);

        List<Marker> GetUsersMarkers(string userId);

        Marker UpdateMarker(Marker marker);
    }
}