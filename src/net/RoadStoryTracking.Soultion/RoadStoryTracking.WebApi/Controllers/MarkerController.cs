using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Marker;
using RoadStoryTracking.WebApi.Business.MarkerService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using BMM = RoadStoryTracking.WebApi.Business.BusinessModels.Marker;

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
            var mappedMarker = LocalMapper.Map<BMM.Marker>(marker);
            var response = _markerService.AddMarker(mappedMarker, Requestor.User.Id);

            return response.GetActionResult<BMM.Marker, Marker>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult DeleteMarker(Guid markerId)
        {
            var response = _markerService.DeleteMarker(markerId, Requestor.User.Id);
            return response.GetActionResult<BMM.Marker, Marker>(this);
        }

        [HttpGet("[action]")]
        public IActionResult GetMarker(Guid markerId)
        {
            var response = _markerService.GetMarker(markerId);
            return response.GetActionResult<BMM.Marker, Marker>(this);
        }

        [HttpGet("[action]")]
        public IActionResult GetMarkers()
        {
            var response = _markerService.GetMarkers();
            return response.GetActionResult<List<BMM.Marker>, List<Marker>>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetMyMarkers()
        {
            var response = _markerService.GetUsersMarkers(Requestor.User.Id);
            return response.GetActionResult<List<BMM.Marker>, List<Marker>>(this);
        }

        [Authorize]
        [HttpPut("[action]")]
        public IActionResult UpdateMarker([FromBody] Marker marker)
        {
            var mappedMarker = LocalMapper.Map<BMM.Marker>(marker);
            var response = _markerService.UpdateMarker(mappedMarker, Requestor.User.Id);

            return response.GetActionResult<BMM.Marker, Marker>(this);
        }
    }
}