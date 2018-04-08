using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class Friend
    {
        public DateTimeOffset CreateDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsAccepted { get; set; }

        [Required]
        public ApplicationUser RequestedBy { get; set; }

        public string RequestedById { get; set; }

        public ApplicationUser RequestedTo { get; set; }

        public string RequestedToId { get; set; }
    }
}