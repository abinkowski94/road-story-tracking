using System.ComponentModel.DataAnnotations;

namespace RoadStoryTracking.WebApi.Business.Models.User
{
    public class ApplicationUser
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}