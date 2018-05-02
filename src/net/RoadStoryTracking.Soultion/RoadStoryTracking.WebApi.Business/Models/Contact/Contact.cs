using System;

namespace RoadStoryTracking.WebApi.Business.Models.Contact
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string Image { get; set; }
        public Guid InvitationId { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}