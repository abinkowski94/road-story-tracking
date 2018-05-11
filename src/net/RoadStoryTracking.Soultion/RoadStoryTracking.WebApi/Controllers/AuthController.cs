using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.User;
using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Threading.Tasks;

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
            if (response is SuccessResponse<object>)
            {
                return Redirect("~/account/register/confirmed");
            }
            else
            {
                return new ObjectResult(null);
            }
        }

        [HttpGet("token")]
        public async Task<IActionResult> CreateToken(string userName, string password)
        {
            var response = await _userService.CreateToken(userName, password);
            return response.GetActionResult<Business.Models.User.TokenInfo, TokenInfo>(this);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] ApplicationUser applicationUser)
        {
            var callbackUri = new Uri($"{Request.Scheme}://{Request.Host}{Url.Action(nameof(ConfirmEmailAddress))}", UriKind.Absolute);
            var user = LocalMapper.Map<Business.Models.User.ApplicationUser>(applicationUser);
            var response = await _userService.RegisterNewUser(user, callbackUri);
            return response.GetActionResult<Business.Models.User.ApplicationUser, ApplicationUser>(this);
        }
    }
}