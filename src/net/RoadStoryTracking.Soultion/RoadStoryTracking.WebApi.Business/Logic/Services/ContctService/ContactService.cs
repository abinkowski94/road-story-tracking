using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
using RoadStoryTracking.WebApi.Business.Models.Contact;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.ContctService
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IUserService _userService;

        public ContactService(IContactsRepository contactsRepository, IUserService userService)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository), $"{nameof(IContactsRepository)} cannot be null!");
            _userService = userService ?? throw new ArgumentNullException(nameof(userService), $"{nameof(IUserService)} cannot be null!");
        }

        public BaseResponse AcceptInvitation(Guid contactId, string userId)
        {
            var contact = _contactsRepository.GetContact(contactId);
            if (contact?.RequestedToId != userId)
            {
                return new ErrorResponse(new ArgumentException($"User with id: {userId} does not own invitation with id ${contactId}"));
            }

            contact.Status = Data.Models.InvitationStatuses.Accepted;
            _contactsRepository.UpdateContact(contact);

            var result = LocalMapper.Map<Contact>(contact.RequestedBy);
            return new SuccessResponse<Contact>(result);
        }

        public BaseResponse DeleteContact(Guid contactId, string userId)
        {
            var contact = _contactsRepository.GetContact(contactId);
            var resultContact = contact.RequestedById == userId ? contact.RequestedTo : contact.RequestedBy;
            var result = LocalMapper.Map<Contact>(resultContact);

            _contactsRepository.DeleteContact(contact);

            return new SuccessResponse<Contact>(result);
        }

        public BaseResponse GetIncomingInvitations(string userId)
        {
            var contactList = _contactsRepository.GetAllContacts(userId)
                .Where(c => c.RequestedToId == userId)
                .Where(c => c.Status == Data.Models.InvitationStatuses.PendingAcceptance).ToList();

            var result = LocalMapper.Map<List<Inviation>>(contactList);
            result.ForEach(c => c.User.InvitationId = contactList.First(cl => cl.RequestedBy.UserName == c.User.UserName || cl.RequestedTo.UserName == c.User.UserName).Id);

            return new SuccessResponse<List<Inviation>>(result);
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

        public BaseResponse GetPotentionalContacts(string userId, string userName)
        {
            var potentionalFriends = _contactsRepository.GetPotentionalContacts(userId, userName);
            var result = LocalMapper.Map<List<Contact>>(potentionalFriends);

            return new SuccessResponse<List<Contact>>(result);
        }

        public async Task<BaseResponse> SendInvitationAsync(string userId, string invitedUserName)
        {
            if (invitedUserName == null)
            {
                return new ErrorResponse(new ArgumentNullException($"{invitedUserName} cannot be null!"));
            }

            var invitedUserResponse = await _userService.GetUser(invitedUserName);
            var invitedUser = (invitedUserResponse as SuccessResponse<ApplicationUser>)?.Result;
            if (invitedUser == null)
            {
                return invitedUserResponse;
            }

            if (invitedUser.Id == userId)
            {
                return new ErrorResponse(new ApplicationException("Cannot send invitation to itself!"));
            }

            var myContactsResponse = GetPotentionalContacts(userId, invitedUserName);
            var myContacts = (myContactsResponse as SuccessResponse<List<Contact>>)?.Result;
            if (myContacts == null)
            {
                return myContactsResponse;
            }

            if (!myContacts.Any(c => c.UserName == invitedUserName))
            {
                return new ErrorResponse(new ApplicationException("Contact already exists!"));
            }

            var newContact = new Data.Models.Contact
            {
                CreateDate = DateTimeOffset.UtcNow,
                RequestedById = userId,
                RequestedToId = invitedUser.Id,
                Status = Data.Models.InvitationStatuses.PendingAcceptance
            };
            _contactsRepository.AddContact(newContact);

            var result = LocalMapper.Map<Contact>(newContact);
            return new SuccessResponse<Contact>(result);
        }
    }
}