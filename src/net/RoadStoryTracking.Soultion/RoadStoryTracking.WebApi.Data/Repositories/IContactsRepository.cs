using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface IContactsRepository
    {
        Contact AddContact(Contact friend);

        Contact DeleteContact(Contact contact);

        List<Contact> GetAcceptedContacts(string userId);

        List<Contact> GetAllContacts(string userId);

        Contact GetContact(Guid contactId);

        List<ApplicationUser> GetPotentionalContacts(string userId, string userName);

        Contact UpdateContact(Contact contact);
    }
}