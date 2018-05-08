using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class Contact
    {
        public DateTimeOffset CreateDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public ApplicationUser RequestedBy { get; set; }

        public string RequestedById { get; set; }

        public ApplicationUser RequestedTo { get; set; }

        public string RequestedToId { get; set; }

        public InvitationStatuses Status { get; set; }
    }
}