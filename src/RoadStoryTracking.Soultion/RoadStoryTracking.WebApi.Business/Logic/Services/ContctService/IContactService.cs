using RoadStoryTracking.WebApi.Business.Models.Responses;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ContctService
{
    public interface IContactService
    {
        BaseResponse AcceptInvitation(Guid contactId, string id);

        BaseResponse DeleteContact(Guid contactId, string userId);

        BaseResponse GetIncomingInvitations(string userId);

        BaseResponse GetMyContacts(string userId);

        BaseResponse GetPotentionalContacts(string userId, string userName);

        Task<BaseResponse> SendInvitationAsync(string userId, string invitedUserName);
    }
}