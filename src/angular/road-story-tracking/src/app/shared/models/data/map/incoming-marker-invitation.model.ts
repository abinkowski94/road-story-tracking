import { InvitationStatuses } from './invitation-statuses.enum.model';
import { MarkerOwner } from './marker-owner.model';

export class IncomingMarkerInviation {
    public description: string;
    public id: string;
    public invitationStatus: InvitationStatuses;
    public markerId: string;
    public markerOwner: MarkerOwner;
    public name: string;
}
