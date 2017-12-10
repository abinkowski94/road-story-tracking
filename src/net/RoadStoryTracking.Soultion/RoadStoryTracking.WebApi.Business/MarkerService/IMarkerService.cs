﻿using RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.MarkerService
{
    public interface IMarkerService
    {
        Task<BaseResponse> AddMarker(Marker marker, string userId);

        Task<BaseResponse> GetMarker(Guid markerId);

        Task<BaseResponse> GetMarkers();
    }
}