using System;
using System.Collections.Generic;
using RoadStoryTracking.WebApi.Data.Models;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface IContactsRepository
    {
        Contact AddContact(Contact friend);

        List<Contact> GetAcceptedContacts(string userId);

        List<Contact> GetAllContacts(string userId);

        Contact GetContact(Guid contactId);

        Contact UpdateContact(Contact contact);
    }
}