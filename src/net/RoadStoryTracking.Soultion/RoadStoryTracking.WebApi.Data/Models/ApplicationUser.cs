using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Comment> Comments { get; set; }

        public List<Contact> Contacts { get; set; }

        public string FirstName { get; set; }

        public string ImageUrl { get; set; }

        public string LastName { get; set; }

        public List<Marker> Markers { get; set; }
    }
}