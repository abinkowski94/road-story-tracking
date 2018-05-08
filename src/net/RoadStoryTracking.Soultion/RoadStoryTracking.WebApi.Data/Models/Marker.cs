using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class Marker
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string Description { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public List<MarkerImage> Images { get; set; }

        public bool IsPrivate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<MarkerInvitation> MarkerInvitations { get; set; }

        public DateTimeOffset? ModificationDate { get; set; }

        public string Name { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public MarkerType Type { get; set; }

        public DateTimeOffset ValidOnMapTo { get; set; }
    }
}