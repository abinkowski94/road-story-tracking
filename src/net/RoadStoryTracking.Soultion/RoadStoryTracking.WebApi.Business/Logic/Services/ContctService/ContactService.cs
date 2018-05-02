using RoadStoryTracking.WebApi.Business.Models.Contact;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ContctService
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository), $"{nameof(IContactsRepository)} cannot be null!");
        }

        public BaseResponse DeleteContact(Guid contactId, string userId)
        {
            var contact = _contactsRepository.GetContact(contactId);
            var resultContact = contact.RequestedById == userId ? contact.RequestedTo : contact.RequestedBy;
            var result = LocalMapper.Map<Contact>(resultContact);

            _contactsRepository.DeleteContact(contact);

            return new SuccessResponse<Contact>(result);
        }

        public BaseResponse GetMyContacts(string userId)
        {
            var contactList = _contactsRepository.GetAcceptedContacts(userId);
            var contacts = contactList.Where(c => c.RequestedById != userId).Select(c => c.RequestedBy)
                .Concat(contactList.Where(c => c.RequestedToId != userId).Select(c => c.RequestedTo))
                .GroupBy(c => c.UserName).Select(g => g.First()).ToList();

            var result = LocalMapper.Map<List<Contact>>(contacts);
            result.ForEach(c => c.InvitationId = contactList.First(cl => cl.RequestedBy.UserName == c.UserName || cl.RequestedTo.UserName == c.UserName).Id);

            return new SuccessResponse<List<Contact>>(result);
        }
    }
}