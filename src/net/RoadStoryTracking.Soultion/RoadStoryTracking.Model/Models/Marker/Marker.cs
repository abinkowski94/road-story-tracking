using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class Marker
    {
        [JsonProperty("createDate")]
        public DateTimeOffset CreateDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty("markerOwner")]
        public MarkerOwner MarkerOwner { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public MarkerType Type { get; set; }
    }
}