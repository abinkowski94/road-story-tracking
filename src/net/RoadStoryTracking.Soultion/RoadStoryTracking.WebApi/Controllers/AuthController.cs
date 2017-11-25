﻿using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models;
using RoadStoryTracking.WebApi.Business.UserService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Threading.Tasks;
using BM = RoadStoryTracking.WebApi.Business.BusinessModels;

namespace RoadStoryTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmailAddress(string userName, string token)
        {
            var response = await _userService.ConfirmUserEmailAddress(userName, token);
            return response.GetActionResult(this);
        }

        [HttpGet("token")]
        public async Task<IActionResult> CreateToken(string userName, string password)
        {
            var response = await _userService.CreateToken(userName, password);
            return response.GetActionResult<BM.TokenInfo, TokenInfo>(this);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] ApplicationUser ApplicationUser)
        {
            var callbackUri = new Uri($"{Request.Scheme}://{Request.Host}{Url.Action(nameof(ConfirmEmailAddress))}", UriKind.Absolute);
            var user = LocalMapper.Map<BM.ApplicationUser>(ApplicationUser);
            var response = await _userService.RegisterNewUser(user, callbackUri);
            return response.GetActionResult<BM.ApplicationUser, ApplicationUser>(this);
        }
    }
}