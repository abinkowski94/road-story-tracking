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
                if (markerToDelete == null || markerToDelete.ApplicationUserId != userId)
                {
                    return new ErrorResponse(new CustomApplicationException("You do not have privileges to delete this event!"));
                }

                var result = LocalMapper.Map<Marker>(markerToDelete);

                result.Images.ForEach(i => _imageService.DeleteImage(i));
                _markerRepository.DeleteMarker(markerToDelete);

                return new SuccessResponse<Marker>(result);
            });
        }

        public Task<BaseResponse> UpdateMarker(Marker marker, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbMarker = _markerRepository.GetMarker(marker.Id);
                if (dbMarker == null || dbMarker.ApplicationUserId != userId)
                {
                    return new ErrorResponse(new CustomApplicationException($"Marker with id: {marker.Id} not found"));
                }

                UpdateDatabaseMarker(marker, dbMarker);

                return new SuccessResponse<Marker>(LocalMapper.Map<Marker>(_markerRepository.UpdateMarker(dbMarker)));
            });
        }

        private Data.Models.Marker UpdateDatabaseMarker(Marker updateModel, Data.Models.Marker markerToUpdate)
        {
            markerToUpdate.Description = updateModel.Description;
            markerToUpdate.Latitude = (double) updateModel.Latitude;
            markerToUpdate.Longitude = (double) updateModel.Longitude;
            markerToUpdate.ModificationDate = DateTimeOffset.UtcNow;
            markerToUpdate.Name = updateModel.Name;
            markerToUpdate.Type = (Data.Models.MarkerType) ((int) updateModel.Type);

            var existingImages = markerToUpdate.Images.Where(img => updateModel.Images.Any(umi => umi == img.Image)).ToList();
            var imagesToRemove = markerToUpdate.Images.Where(img => existingImages.Any(ei => ei.Id == img.Id)).ToList();
            var imagesToCreate = updateModel.Images.Where(img => !existingImages.Any(ei => ei.Image == img)).ToList();

            // Remove old images
            imagesToRemove.ForEach(img => _imageService.DeleteImage(img.Image));
            _markerRepository.DeleteMarkerImages(imagesToRemove);

            // Create new images
            var imagesToAppend = imagesToCreate.Select(img => new Data.Models.MarkerImage
            {
                Image = _imageService.SaveImage(img, Guid.NewGuid().ToString(), _markerImagesLocation),
                CreateDate = DateTimeOffset.UtcNow
            }).ToList();
            markerToUpdate.Images.AddRange(imagesToAppend);

            return markerToUpdate;
        }
    }
}