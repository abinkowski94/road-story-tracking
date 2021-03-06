﻿using Microsoft.EntityFrameworkCore;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public class MarkerRepository : IMarkerRepository
    {
        private readonly RoadStoryTrackingDbContext _dbContext;

        public MarkerRepository(RoadStoryTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Marker AddMarker(Marker marker)
        {
            _dbContext.Markers.Add(marker);
            _dbContext.SaveChanges();

            return marker;
        }

        public List<MarkerInvitation> AddMarkerInvitations(List<MarkerInvitation> markerInvitations)
        {
            _dbContext.MarkerInvitations.AddRange(markerInvitations);
            _dbContext.SaveChanges();

            return markerInvitations;
        }

        public Marker DeleteMarker(Marker marker)
        {
            if (marker.Images.Any())
            {
                _dbContext.MarkerImages.RemoveRange(marker.Images);
            }
            if (marker.MarkerInvitations.Any())
            {
                _dbContext.MarkerInvitations.RemoveRange(marker.MarkerInvitations);
            }
            _dbContext.Markers.Remove(marker);
            _dbContext.SaveChanges();

            return marker;
        }

        public List<MarkerImage> DeleteMarkerImages(List<MarkerImage> markerImages)
        {
            _dbContext.MarkerImages.RemoveRange(markerImages);
            _dbContext.SaveChanges();

            return markerImages;
        }

        public MarkerInvitation DeleteMarkerInvitation(string userId, Guid invitationId)
        {
            var invitation = GetIncomingMarkersInvitation(userId, invitationId);
            if (invitation == null)
            {
                return invitation;
            }

            _dbContext.MarkerInvitations.Remove(invitation);
            _dbContext.SaveChanges();

            return invitation;
        }

        public List<MarkerInvitation> DeleteMarkerInvitations(List<MarkerInvitation> markerInvitations)
        {
            _dbContext.MarkerInvitations.RemoveRange(markerInvitations);
            _dbContext.SaveChanges();

            return markerInvitations;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public MarkerInvitation GetIncomingMarkersInvitation(string userId, Guid invitationId)
        {
            return _dbContext.MarkerInvitations
                .Include(i => i.InvitedUser)
                .Include(i => i.Marker)
                .ThenInclude(m => m.ApplicationUser)
                .FirstOrDefault(mi => mi.InvitedUserId == userId && mi.Id == invitationId);
        }

        public List<MarkerInvitation> GetIncomingMarkersInvitations(string userId)
        {
            return _dbContext.MarkerInvitations
                .Include(i => i.InvitedUser)
                .Include(i => i.Marker)
                .ThenInclude(m => m.ApplicationUser)
                .Where(i => i.InvitedUser.Id == userId)
                .ToList();
        }

        public Marker GetMarker(Guid markerId)
        {
            return _dbContext.Markers
                .Include(m => m.Images)
                .Include(m => m.ApplicationUser)
                .Include(m => m.MarkerInvitations)
                .ThenInclude(mi => mi.InvitedUser)
                .FirstOrDefault(m => m.Id == markerId);
        }

        public List<Marker> GetMarkers(string userId)
        {
            return _dbContext.Markers
                .Include(m => m.Images)
                .Include(m => m.ApplicationUser)
                .Include(m => m.MarkerInvitations)
                .ThenInclude(mi => mi.InvitedUser)
                .Where(m => m.EndDate > DateTimeOffset.Now)
                .Where(m => !m.IsPrivate || (m.IsPrivate && m.ApplicationUserId == userId)
                    || (m.IsPrivate && m.MarkerInvitations.Any(mi => mi.InvitedUserId == userId)))
                .ToList();
        }

        public Dictionary<string, ApplicationUser> GetUsersDictionary(List<string> userNames)
        {
            return _dbContext.Users
                .Where(u => userNames.Contains(u.UserName))
                .ToDictionary(u => u.UserName, u => u);
        }

        public List<Marker> GetUsersMarkers(string userId)
        {
            return _dbContext.Markers
                .Include(m => m.Images)
                .Include(m => m.MarkerInvitations)
                .ThenInclude(mi => mi.InvitedUser)
                .Where(m => m.ApplicationUserId == userId)
                .ToList();
        }

        public MarkerInvitation UpdateIncomingMarkersInvitation(MarkerInvitation invitation)
        {
            _dbContext.SaveChanges();
            return invitation;
        }

        public Marker UpdateMarker(Marker marker)
        {
            _dbContext.SaveChanges();
            return marker;
        }
    }
}