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

        MarkerInvitation DeleteMarkerInvitation(string userId, Guid invitationId);

        List<MarkerInvitation> DeleteMarkerInvitations(List<MarkerInvitation> markerInvitations);

        MarkerInvitation GetIncomingMarkersInvitation(string userId, Guid invitationId);

        List<MarkerInvitation> GetIncomingMarkersInvitations(string userId);

        Marker GetMarker(Guid markerId);

        List<Marker> GetMarkers(string userId);

        Dictionary<string, ApplicationUser> GetUsersDictionary(List<string> userIds);

        List<Marker> GetUsersMarkers(string userId);

        MarkerInvitation UpdateIncomingMarkersInvitation(MarkerInvitation invitation);

        Marker UpdateMarker(Marker marker);
    }
}