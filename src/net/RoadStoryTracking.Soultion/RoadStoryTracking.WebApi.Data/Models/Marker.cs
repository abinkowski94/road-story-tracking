﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadStoryTracking.WebApi.Data.Models
{
    public class Marker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public MarkerType Type { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ModificationDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public List<MarkerImage> Images { get; set; }
    }
}