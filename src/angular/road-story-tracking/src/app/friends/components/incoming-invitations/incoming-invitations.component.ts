import { Component, OnInit } from '@angular/core';

import { DialogService } from '../../../shared/services/dialog/dialog.service';
import { Invitation } from '../../../shared/models/data/friends/invitation.model';

@Component({
    selector: 'app-incoming-invitations',
    templateUrl: './incoming-invitations.component.html',
    styleUrls: ['./incoming-invitations.component.css']
})
export class IncomingInvitationsComponent implements OnInit {

    public invitations: Invitation[];

    public constructor(private dialogService: DialogService) { }

    public ngOnInit(): void {
        this.invitations = [
            {
                user: {
                    firstName: 'John',
                    lastName: 'Doe',
                    userName: 'john.doe@test.ts',
                    image: null
                },
                sendDate: new Date()
            },
            {
                user: {
                    firstName: 'Janusz',
                    lastName: 'Kowalski',
                    userName: 'janusz.kowalski@test.ts',
                    image: null
                },
                sendDate: new Date()
            }
        ];
    }

    public async acceptInvitation(invitation: Invitation): Promise<void> {
        const result = await this.dialogService
            .confirm('Accept invitation', `Do you want to accept inviation from ${invitation.user.userName}?`).toPromise();

        if (result) {
            this.removeInvitation(invitation);
        }
    }

    public async declineInvitation(invitation: Invitation): Promise<void> {
        const result = await this.dialogService
            .confirm('Decline invitation', `Do you want to decline inviation from ${invitation.user.userName}?`).toPromise();

        if (result) {
            this.removeInvitation(invitation);
        }
    }

    private removeInvitation(invitation: Invitation): void {
        const index = this.invitations.indexOf(invitation);
        if (index > -1) {
            this.invitations.splice(index, 1);
        }
    }
}
