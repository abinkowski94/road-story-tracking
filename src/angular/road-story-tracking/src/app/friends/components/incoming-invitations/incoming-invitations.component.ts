import { Component, OnInit } from '@angular/core';

import { DialogService } from '../../../shared/services/dialog/dialog.service';
import { Invitation } from '../../../shared/models/data/friends/invitation.model';
import { FriendsApiService } from '../../services/friends-api.service';

@Component({
    selector: 'app-incoming-invitations',
    templateUrl: './incoming-invitations.component.html',
    styleUrls: ['./incoming-invitations.component.css']
})
export class IncomingInvitationsComponent implements OnInit {

    public invitations: Invitation[];

    public constructor(private dialogService: DialogService, private friendsApiService: FriendsApiService) { }

    public ngOnInit(): void {
        this.friendsApiService.getInvitations().subscribe(invitations => this.invitations = invitations);
    }

    public async acceptInvitation(invitation: Invitation): Promise<void> {
        const result = await this.dialogService
            .confirm('Accept invitation', `Do you want to accept inviation from ${invitation.user.userName}?`).toPromise();

        if (result) {
            try {
                await this.friendsApiService.acceptInvitation(invitation.user.invitationId).toPromise();
                this.removeInvitation(invitation);
            } catch (exception) { }
        }
    }

    public async declineInvitation(invitation: Invitation): Promise<void> {
        const result = await this.dialogService
            .confirm('Decline invitation', `Do you want to decline inviation from ${invitation.user.userName}?`).toPromise();

        if (result) {
            try {
                await this.friendsApiService.deleteFriend(invitation.user.invitationId).toPromise();
                this.removeInvitation(invitation);
            } catch (exception) { }
        }
    }

    private removeInvitation(invitation: Invitation): void {
        const index = this.invitations.indexOf(invitation);
        if (index > -1) {
            this.invitations.splice(index, 1);
        }
    }
}
