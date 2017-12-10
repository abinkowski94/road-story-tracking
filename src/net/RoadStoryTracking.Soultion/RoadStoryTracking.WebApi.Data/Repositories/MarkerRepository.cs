using Microsoft.EntityFrameworkCore;
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

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Marker GetMarker(Guid markerId)
        {
            return _dbContext.Markers
                .Include(m => m.Images)
                .Include(m => m.ApplicationUser)
                .FirstOrDefault(m => m.Id == markerId);
        }

        public List<Marker> GetMarkers()
        {
            return _dbContext.Markers
                .Include(m => m.Images)
                .Include(m => m.ApplicationUser)
                .Where(m => m.CreateDate > DateTimeOffset.Now.AddDays(-1))
                .ToList();
        }

        public List<Marker> GetUsersMarkers(string userId)
        {
            return _dbContext.Markers
                .Where(m => m.ApplicationUserId == userId)
                .ToList();
        }
    }
}