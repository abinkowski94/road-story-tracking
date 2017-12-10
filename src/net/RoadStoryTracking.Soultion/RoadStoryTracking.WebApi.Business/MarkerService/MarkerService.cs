﻿using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.MarkerService
{
    public class MarkerService : BaseService, IMarkerService
    {
        private readonly IMarkerRepository _markerRepository;

        public MarkerService(IMarkerRepository markerRepository)
        {
            _markerRepository = markerRepository;
        }

        public Task<BaseResponse> AddMarker(Marker marker, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbMarker = LocalMapper.Map<Data.Models.Marker>(marker);
                dbMarker.ApplicationUserId = userId;
                dbMarker = _markerRepository.AddMarker(dbMarker);

                var result = LocalMapper.Map<Marker>(dbMarker);
                return new SuccessResponse<Marker>(result);
            });
        }

        public Task<BaseResponse> GetMarker(Guid markerId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbMarker = _markerRepository.GetMarker(markerId);
                if (dbMarker == null)
                {
                    return new ErrorResponse(new CustomApplicationException($"Marker with id: {markerId} not found"));
                }

                var result = LocalMapper.Map<Marker>(dbMarker);
                return new SuccessResponse<Marker>(result);
            });
        }

        public Task<BaseResponse> GetMarkers()
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbMarkers = _markerRepository.GetMarkers();
                var result = LocalMapper.Map<List<Marker>>(dbMarkers);

                return new SuccessResponse<List<Marker>>(result);
            });
        }
    }
}