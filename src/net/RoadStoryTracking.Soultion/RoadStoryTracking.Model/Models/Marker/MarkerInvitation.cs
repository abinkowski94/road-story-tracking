using Newtonsoft.Json;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class MarkerInvitation
    {
        [JsonProperty("invitationStatus")]
        public InvitationStatuses InvitationStatus { get; set; }

        [JsonProperty("invitedUserFirstName")]
        public string InvitedUserFirstName { get; set; }

        [JsonProperty("invitedUserImage")]
        public string InvitedUserImage { get; set; }

        [JsonProperty("invitedUserLastName")]
        public string InvitedUserLastName { get; set; }

        [JsonProperty("invitedUserUserName")]
        public string InvitedUserUserName { get; set; }
    }
}