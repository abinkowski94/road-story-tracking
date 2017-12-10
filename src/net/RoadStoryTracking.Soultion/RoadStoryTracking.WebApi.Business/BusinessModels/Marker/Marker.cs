﻿using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Business.BusinessModels.Marker
{
    public class Marker
    {
        public DateTimeOffset CreateDate { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public List<string> Images { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public MarkerOwner MarkerOwner { get; set; }
        public string Name { get; set; }
        public MarkerType Type { get; set; }
    }
}