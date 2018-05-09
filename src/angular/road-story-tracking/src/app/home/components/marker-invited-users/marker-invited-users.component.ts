import { Component, Input } from '@angular/core';

import { MarkerInvitation } from './../../../shared/models/data/map/marker-invitation.model';

@Component({
    templateUrl: 'marker-invited-users.component.html',
    styleUrls: ['marker-invited-users.component.css'],
    selector: 'app-marker-invited-users'
})
export class MarkerInvitedUsersComponent {

    @Input() public invitations: MarkerInvitation[];
}
