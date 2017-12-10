using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.WebApi.Business.ImageService;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.MarkerService
{
    public class MarkerService : BaseService, IMarkerService
    {
        private const string _markerImagesLocation = "markers/images";
        private readonly IMarkerRepository _markerRepository;
        private readonly IImageService _imageService;

        public MarkerService(IMarkerRepository markerRepository, IImageService imageService)
        {
            _markerRepository = markerRepository;
            _imageService = imageService;
        }

        public Task<BaseResponse> AddMarker(Marker marker, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                marker.Images = marker.Images.Select(i => _imageService.SaveImage(i, Guid.NewGuid().ToString(), _markerImagesLocation)).ToList();
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

        public Task<BaseResponse> GetUsersMarkers(string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbMarkers = _markerRepository.GetUsersMarkers(userId);
                var result = LocalMapper.Map<List<Marker>>(dbMarkers);

                return new SuccessResponse<List<Marker>>(result);
            });
        }

        public Task<BaseResponse> DeleteMarker(Guid markerId, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var markerToDelete = _markerRepository.GetMarker(markerId);
                if(markerToDelete == null || markerToDelete.ApplicationUserId != userId)
                {
                    return new ErrorResponse(new CustomApplicationException("You do not have privileges to delete this event!"));
                }

                var result = LocalMapper.Map<Marker>(markerToDelete);

                result.Images.ForEach(i => _imageService.DeleteImage(i));
                _markerRepository.DeleteMarker(markerToDelete);

                return new SuccessResponse<Marker>(result);
            });
        }
    }
}