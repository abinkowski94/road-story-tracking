import { InvitationStatuses } from './invitation-statuses.enum.model';

export class MarkerInvitation {
    public invitedUserFirstName: string;
    public invitedUserImage: string;
    public invitedUserLastName: string;
    public invitedUserUserName: string;
    public invitationStatus: InvitationStatuses;
}
