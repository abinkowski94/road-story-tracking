using RoadStoryTracking.WebApi.Business.Models.Responses;
using System;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ContctService
{
    public interface IContactService
    {
        BaseResponse DeleteContact(Guid contactId, string userId);

        BaseResponse GetMyContacts(string userId);
    }
}