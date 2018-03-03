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

        public Marker DeleteMarker(Marker marker)
        {
            if (marker.Images.Any())
            {
                _dbContext.MarkerImages.RemoveRange(marker.Images);
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
                .Include(m => m.Images)
                .Where(m => m.ApplicationUserId == userId)
                .ToList();
        }

        public Marker UpdateMarker(Marker marker)
        {
            _dbContext.SaveChanges();
            return marker;
        }
    }
}