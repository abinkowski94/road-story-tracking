using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Contact;
using RoadStoryTracking.WebApi.Business.Logic.Services.ContctService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;

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
        [HttpDelete("[action]")]
        public IActionResult DeleteContact(Guid contactId)
        {
            var response = _contactService.DeleteContact(contactId, Requestor.User.Id);
            return response.GetActionResult<Business.Models.Contact.Contact, Contact>(this);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetMyContacts()
        {
            var response = _contactService.GetMyContacts(Requestor.User.Id);
            return response.GetActionResult<List<Business.Models.Contact.Contact>, List<Contact>>(this);
        }
    }
}