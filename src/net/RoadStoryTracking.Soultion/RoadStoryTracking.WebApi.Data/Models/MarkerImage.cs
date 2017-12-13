using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class MarkerImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Image { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ModificationDate { get; set; }

        public Marker Marker { get; set; }
        public Guid MarkerId { get; set; }
    }
}