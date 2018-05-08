using RoadStoryTracking.WebApi.Business.Logic.Services.ImageService;
using RoadStoryTracking.WebApi.Business.Models.Exceptions;
using RoadStoryTracking.WebApi.Business.Models.Marker;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.MarkerService
{
    public class MarkerService : BaseService, IMarkerService
    {
        private const string _markerImagesLocation = "markers\\images";
        private readonly IImageService _imageService;
        private readonly IMarkerRepository _markerRepository;

        public MarkerService(IMarkerRepository markerRepository, IImageService imageService)
        {
            _markerRepository = markerRepository;
            _imageService = imageService;
        }

        public BaseResponse AddMarker(Marker marker, string userId)
        {
            // Create images
            marker.Images = marker.Images.Select(i => _imageService.SaveImageAsync(i, Guid.NewGuid().ToString(), _markerImagesLocation).Result).ToList();

            // Add marker to database
            var dbMarker = LocalMapper.Map<Data.Models.Marker>(marker);
            dbMarker.ApplicationUserId = userId;
            dbMarker = _markerRepository.AddMarker(dbMarker);

            // Add marker invitations to database
            var markerInvitations = new List<Data.Models.MarkerInvitation>();
            if (marker.MarkerInvitations != null && marker.MarkerInvitations.Any())
            {
                markerInvitations = CreateMarkerInvitations(dbMarker, marker.MarkerInvitations);
                markerInvitations = _markerRepository.AddMarkerInvitations(markerInvitations);
            }

            // Return mapped result
            var result = LocalMapper.Map<Marker>(dbMarker);
            result.MarkerInvitations = LocalMapper.Map<List<MarkerInvitation>>(markerInvitations);
            return new SuccessResponse<Marker>(result);
        }

        public BaseResponse DeleteMarker(Guid markerId, string userId)
        {
            var markerToDelete = _markerRepository.GetMarker(markerId);
            if (markerToDelete == null || markerToDelete.ApplicationUserId != userId)
            {
                return new ErrorResponse(new CustomApplicationException($"Marker with id: {markerId} not found"));
            }

            var result = LocalMapper.Map<Marker>(markerToDelete);
            result.Images.ForEach(i => _imageService.DeleteImageAsync(i));

            _markerRepository.DeleteMarker(markerToDelete);

            return new SuccessResponse<Marker>(result);
        }

        public BaseResponse GetMarker(Guid markerId)
        {
            var dbMarker = _markerRepository.GetMarker(markerId);
            if (dbMarker == null)
            {
                return new ErrorResponse(new CustomApplicationException($"Marker with id: {markerId} not found"));
            }

            var result = LocalMapper.Map<Marker>(dbMarker);
            return new SuccessResponse<Marker>(result);
        }

        public BaseResponse GetMarkers()
        {
            var dbMarkers = _markerRepository.GetMarkers();
            var result = LocalMapper.Map<List<Marker>>(dbMarkers);

            return new SuccessResponse<List<Marker>>(result);
        }

        public BaseResponse GetUsersMarkers(string userId)
        {
            var dbMarkers = _markerRepository.GetUsersMarkers(userId);
            var result = LocalMapper.Map<List<Marker>>(dbMarkers);

            return new SuccessResponse<List<Marker>>(result);
        }

        public BaseResponse UpdateMarker(Marker marker, string userId)
        {
            var dbMarker = _markerRepository.GetMarker(marker.Id);
            if (dbMarker == null || dbMarker.ApplicationUserId != userId)
            {
                return new ErrorResponse(new CustomApplicationException($"Marker with id: {marker.Id} not found"));
            }

            UpdateDatabaseMarker(marker, dbMarker);

            var result = _markerRepository.UpdateMarker(dbMarker);
            return new SuccessResponse<Marker>(LocalMapper.Map<Marker>(result));
        }

        private List<Data.Models.MarkerInvitation> CreateMarkerInvitations(Data.Models.Marker dbMarker, List<MarkerInvitation> markerInvitations)
        {
            var invitedUsers = _markerRepository.GetUsersDictionary(markerInvitations.Select(mi => mi.InvitedUserUserName).ToList());
            var result = new List<Data.Models.MarkerInvitation>();

            foreach (var markerInvitation in markerInvitations)
            {
                result.Add(new Data.Models.MarkerInvitation
                {
                    InvitationStatuses = Data.Models.InvitationStatuses.PendingAcceptance,
                    Marker = dbMarker,
                    MarkerId = dbMarker.Id,
                    InvitedUser = invitedUsers[markerInvitation.InvitedUserUserName],
                    InvitedUserId = invitedUsers[markerInvitation.InvitedUserUserName].Id
                });
            }

            return result;
        }

        private Data.Models.Marker UpdateDatabaseMarker(Marker updateModel, Data.Models.Marker markerToUpdate)
        {
            markerToUpdate.Description = updateModel.Description;
            markerToUpdate.Latitude = (double)updateModel.Latitude;
            markerToUpdate.Longitude = (double)updateModel.Longitude;
            markerToUpdate.ModificationDate = DateTimeOffset.UtcNow;
            markerToUpdate.Name = updateModel.Name;
            markerToUpdate.Type = (Data.Models.MarkerType)((int)updateModel.Type);

            var existingImages = markerToUpdate.Images.Where(img => updateModel.Images.Any(umi => umi == img.Image)).ToList();
            var imagesToRemove = markerToUpdate.Images.Where(img => !existingImages.Any(ei => ei.Id == img.Id)).ToList();
            var imagesToCreate = updateModel.Images.Where(img => !existingImages.Any(ei => ei.Image == img)).ToList();

            // Remove old images
            imagesToRemove.ForEach(img => _imageService.DeleteImageAsync(img.Image));
            _markerRepository.DeleteMarkerImages(imagesToRemove);

            // Create new images
            var imagesToAppend = imagesToCreate.Select(img => new Data.Models.MarkerImage
            {
                Image = _imageService.SaveImageAsync(img, Guid.NewGuid().ToString(), _markerImagesLocation).Result,
                CreateDate = DateTimeOffset.UtcNow
            }).ToList();
            markerToUpdate.Images.AddRange(imagesToAppend);

            return markerToUpdate;
        }
    }
}