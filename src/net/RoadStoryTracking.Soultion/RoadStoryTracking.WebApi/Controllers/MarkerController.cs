using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Marker;
using RoadStoryTracking.WebApi.Business.MarkerService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> AddMarker([FromBody] Marker marker)
        {
            var mappedMarker = LocalMapper.Map<BMM.Marker>(marker);
            var response = await _markerService.AddMarker(mappedMarker, Requestor.User.Id);

            return response.GetActionResult<BMM.Marker, Marker>(this);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarker(Guid markerId)
        {
            var response = await _markerService.GetMarker(markerId);
            return response.GetActionResult<BMM.Marker, Marker>(this);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarkers()
        {
            var response = await _markerService.GetMarkers();
            return response.GetActionResult<List<BMM.Marker>, List<Marker>>(this);
        }
    }
}