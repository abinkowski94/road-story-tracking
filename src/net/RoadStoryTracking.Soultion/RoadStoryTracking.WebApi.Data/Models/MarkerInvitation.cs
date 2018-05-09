using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class MarkerInvitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public InvitationStatuses InvitationStatus { get; set; }

        public ApplicationUser InvitedUser { get; set; }

        public string InvitedUserId { get; set; }

        public Marker Marker { get; set; }
        public Guid MarkerId { get; set; }
    }
}