namespace RoadStoryTracking.WebApi.Business.Models.Marker
{
    public class MarkerInvitation
    {
        public string InvitedUserFirstName { get; set; }

        public string InvitedUserImage { get; set; }

        public string InvitedUserLastName { get; set; }

        public string InvitedUserUserName { get; set; }

        public bool IsAccepted { get; set; }
    }
}