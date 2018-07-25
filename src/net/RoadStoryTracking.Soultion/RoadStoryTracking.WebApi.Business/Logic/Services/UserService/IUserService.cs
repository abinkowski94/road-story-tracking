using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.UserService
{
    public interface IUserService : IDisposable
    {
        Task<BaseResponse> ConfirmUserEmailAddress(string userName, string confirmationToken);

        Task<BaseResponse> CreateToken(string username, string password);

        Task<BaseResponse> GetUser(string userName);

        Task<BaseResponse> RegisterNewUser(ApplicationUser applicationUser, Uri tokenCallback);

        Task<BaseResponse> ResetPassword(string userName, Uri callbackUri);

        Task<BaseResponse> UpdateUser(string userName, ApplicationUser applicationUser);

        Task<BaseResponse> UpdateUserPassword(string userName, string token, string newPassword);
    }
}