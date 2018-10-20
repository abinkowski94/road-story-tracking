namespace RoadStoryTracking.WebApi.Business.Models.Marker
{
    public class MarkerInvitation
    {
        public InvitationStatuses InvitationStatus { get; set; }

        public string InvitedUserFirstName { get; set; }

        public string InvitedUserImage { get; set; }

        public string InvitedUserLastName { get; set; }

        public string InvitedUserUserName { get; set; }
    }
}