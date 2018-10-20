import { Component, Input } from '@angular/core';

import { MarkerInvitation } from './../../../shared/models/data/map/marker-invitation.model';
import { InvitationStatuses } from '../../../shared/models/data/map/invitation-statuses.enum.model';

@Component({
    templateUrl: 'marker-invited-users.component.html',
    styleUrls: ['marker-invited-users.component.css'],
    selector: 'app-marker-invited-users'
})
export class MarkerInvitedUsersComponent {

    @Input() public invitations: MarkerInvitation[];

    public get acceptedInvitations(): MarkerInvitation[] {
        return this.invitations.filter(i => i.invitationStatus === InvitationStatuses.Accepted);
    }

    public get declinedInvitations(): MarkerInvitation[] {
        return this.invitations.filter(i => i.invitationStatus === InvitationStatuses.Declined);
    }

    public get pendingAcceptanceInvitations(): MarkerInvitation[] {
        return this.invitations.filter(i => i.invitationStatus === InvitationStatuses.PendingAcceptance);
    }
}
