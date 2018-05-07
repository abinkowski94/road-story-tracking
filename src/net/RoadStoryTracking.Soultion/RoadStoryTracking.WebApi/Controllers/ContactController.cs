using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Contact;
using RoadStoryTracking.WebApi.Business.Logic.Services.ContctService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IServiceProvider serviceProvider, IContactService contactService) : base(serviceProvider)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService), $"{nameof(IContactService)} cannot be null!");
        }

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult AcceptInvitation(Guid contactId)
        {
            var response = _contactService.AcceptInvitation(contactId, Requestor.User.Id);
            return response.GetActionResult<Business.Models.Contact.Contact, Contact>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult DeleteContact(Guid contactId)
        {
            var response = _contactService.DeleteContact(contactId, Requestor.User.Id);
            return response.GetActionResult<Business.Models.Contact.Contact, Contact>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetIncomingInvitations()
        {
            var response = _contactService.GetIncomingInvitations(Requestor.User.Id);
            return response.GetActionResult<List<Business.Models.Contact.Inviation>, List<Inviation>>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetMyContacts()
        {
            var response = _contactService.GetMyContacts(Requestor.User.Id);
            return response.GetActionResult<List<Business.Models.Contact.Contact>, List<Contact>>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetPotentionalContacts(string userName)
        {
            var response = _contactService.GetPotentionalContacts(Requestor.User.Id, userName);
            return response.GetActionResult<List<Business.Models.Contact.Contact>, List<Contact>>(this);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendInvitation(string invitedUserName)
        {
            var response = await _contactService.SendInvitationAsync(Requestor.User.Id, invitedUserName);
            return response.GetActionResult<Business.Models.Contact.Contact, Contact>(this);
        }
    }
}