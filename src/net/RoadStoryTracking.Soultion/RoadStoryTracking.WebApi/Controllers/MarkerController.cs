using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Marker;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using RoadStoryTracking.WebApi.Business.Logic.Services.MarkerService;

namespace RoadStoryTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MarkerController : BaseController
    {
        private readonly IMarkerService _markerService;

        public MarkerController(IServiceProvider serviceProvider, IMarkerService markerService) : base(serviceProvider)
        {
            _markerService = markerService;
        }

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult AddMarker([FromBody] Marker marker)
        {
            var mappedMarker = LocalMapper.Map<Business.Models.Marker.Marker>(marker);
            var response = _markerService.AddMarker(mappedMarker, Requestor.User.Id);

            return response.GetActionResult<Business.Models.Marker.Marker, Marker>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult DeleteMarker(Guid markerId)
        {
            var response = _markerService.DeleteMarker(markerId, Requestor.User.Id);
            return response.GetActionResult<Business.Models.Marker.Marker, Marker>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult DeleteMarkerInvitation(Guid invitationId)
        {
            var response = _markerService.DeleteMarkerInvitation(Requestor.User.Id, invitationId);
            return response.GetActionResult<Business.Models.Marker.IncomingMarkerInviation, IncomingMarkerInviation>(this);
        }

        [HttpGet("[action]")]
        public IActionResult GetMarker(Guid markerId)
        {
            var response = _markerService.GetMarker(markerId);
            return response.GetActionResult<Business.Models.Marker.Marker, Marker>(this);
        }

        [HttpGet("[action]")]
        public IActionResult GetMarkers()
        {
            var response = _markerService.GetMarkers();
            return response.GetActionResult<List<Business.Models.Marker.Marker>, List<Marker>>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetMyIncomingMarkersInvitations()
        {
            var response = _markerService.GetIncomingMarkersInvitations(Requestor.User.Id);
            return response.GetActionResult<List<Business.Models.Marker.IncomingMarkerInviation>, List<IncomingMarkerInviation>>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetMyMarkers()
        {
            var response = _markerService.GetUsersMarkers(Requestor.User.Id);
            return response.GetActionResult<List<Business.Models.Marker.Marker>, List<Marker>>(this);
        }

        [Authorize]
        [HttpPut("[action]")]
        public IActionResult UpdateMarker([FromBody] Marker marker)
        {
            var mappedMarker = LocalMapper.Map<Business.Models.Marker.Marker>(marker);
            var response = _markerService.UpdateMarker(mappedMarker, Requestor.User.Id);

            return response.GetActionResult<Business.Models.Marker.Marker, Marker>(this);
        }

        [Authorize]
        [HttpPut("[action]")]
        public IActionResult UpdateMarkerInvitationStatus(Guid invitationId, InvitationStatuses invitationStatus)
        {
            var response = _markerService.UpdateMarkerInvitationStatus(Requestor.User.Id, invitationId, (Business.Models.Marker.InvitationStatuses)((int)invitationStatus));
            return response.GetActionResult<Business.Models.Marker.IncomingMarkerInviation, IncomingMarkerInviation>(this);
        }
    }
}