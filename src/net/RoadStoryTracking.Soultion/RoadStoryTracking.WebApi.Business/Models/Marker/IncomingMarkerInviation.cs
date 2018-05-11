using System;

namespace RoadStoryTracking.WebApi.Business.Models.Marker
{
    public class IncomingMarkerInviation
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public InvitationStatuses InvitationStatus { get; set; }

        public Guid MarkerId { get; set; }

        public MarkerOwner MarkerOwner { get; set; }

        public string Name { get; set; }
    }
}