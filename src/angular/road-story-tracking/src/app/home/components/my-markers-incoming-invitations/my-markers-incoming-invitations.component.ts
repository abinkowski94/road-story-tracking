import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';

import { MarkerApiService } from '../../services/marker-api.service';
import { IncomingMarkerInviation } from '../../../shared/models/data/map/incoming-marker-invitation.model';
import { InvitationStatuses } from '../../../shared/models/data/map/invitation-statuses.enum.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';
import { DialogService } from '../../../shared/services/dialog/dialog.service';

@Component({
    templateUrl: 'my-markers-incoming-invitations.component.html',
    styleUrls: ['my-markers-incoming-invitations.component.css'],
    selector: 'app-my-markers-incoming-invitations'
})
export class MyMarkersIncomingInvitationsComponent implements OnInit {

    public invitations: IncomingMarkerInviation[];

    public constructor(private snackBar: MatSnackBar, private markerApiService: MarkerApiService, private dialogService: DialogService) {
    }

    public ngOnInit(): void {
        this.markerApiService.getIncomingInvitations().subscribe(invitations => this.invitations = invitations);
    }

    public async updateInvitationStatus(invitation: IncomingMarkerInviation, value: InvitationStatuses): Promise<void> {
        if (value === 4) {
            this.dialogService
                .confirm('Delete invitation', `Do you really want to delete invitation to the: '${invitation.name}'?`)
                .subscribe(async dialogResult => {
                    if (dialogResult) {
                        try {
                            const result = await this.markerApiService.deleteInvitation(invitation.id).toPromise();
                            this.invitations.splice(this.invitations.indexOf(invitation), 1);
                            this.snackBar.open('Invitation deleted.', 'Success!', snackbarConfiguration);
                        } catch (exception) { }
                    } else {
                        invitation.invitationStatus = InvitationStatuses.PendingAcceptance;
                    }
                });
        } else {
            try {
                const result = await this.markerApiService.updateMarkerInvitation(invitation.id, value).toPromise();
                this.snackBar.open('Updated invitation status.', 'Success!', snackbarConfiguration);
            } catch (exception) { }
        }
    }

    public get spaceForInvitationSender(): boolean {
        return window.screen.width > 768;
    }
}
