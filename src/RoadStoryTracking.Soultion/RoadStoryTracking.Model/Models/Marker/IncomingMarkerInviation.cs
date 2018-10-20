using Newtonsoft.Json;
using System;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class IncomingMarkerInviation
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("invitationStatus")]
        public InvitationStatuses InvitationStatus { get; set; }

        [JsonProperty("markerId")]
        public Guid MarkerId { get; set; }

        [JsonProperty("markerOwner")]
        public MarkerOwner MarkerOwner { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}