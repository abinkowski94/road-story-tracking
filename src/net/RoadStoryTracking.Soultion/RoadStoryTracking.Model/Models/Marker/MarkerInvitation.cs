using Newtonsoft.Json;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class MarkerInvitation
    {
        [JsonProperty("invitedUserFirstName")]
        public string InvitedUserFirstName { get; set; }

        [JsonProperty("invitedUserImage")]
        public string InvitedUserImage { get; set; }

        [JsonProperty("invitedUserLastName")]
        public string InvitedUserLastName { get; set; }

        [JsonProperty("invitedUserUserName")]
        public string InvitedUserUserName { get; set; }

        [JsonProperty("isAccepted")]
        public bool IsAccepted { get; set; }
    }
}