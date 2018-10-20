import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';

import { MarkerInvitation } from './../../../shared/models/data/map/marker-invitation.model';
import { InvitationStatuses } from './../../../shared/models/data/map/invitation-statuses.enum.model';
import { Friend } from './../../../shared/models/data/friends/friend.model';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { DialogService } from '../../../shared/services/dialog/dialog.service';
import { FriendsApiService } from '../../../friends/services/friends-api.service';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: './marker-invitation-menu.component.html',
    styleUrls: ['./marker-invitation-menu.component.css']
})
export class MarkerInvitationMenuComponent implements OnInit {

    public invitedFriends: MarkerInvitation[];
    public invitedFriendsAccepted: MarkerInvitation[];
    public invitedFriendsDeclined: MarkerInvitation[];

    public friendsCtrl: FormControl;
    public filtratedFriends: Observable<Friend[]>;
    public friends: Observable<Friend[]>;

    public constructor(private dialogRef: MatDialogRef<MarkerInvitationMenuComponent>,
        @Inject(MAT_DIALOG_DATA) private data: MarkerInvitation[], private dialogService: DialogService,
        private snackBar: MatSnackBar, private friendsApiService: FriendsApiService) {

        this.friendsCtrl = new FormControl();
        this.assingFriendsToGroups(data);
    }

    public get allInvitations(): MarkerInvitation[] {
        return this.invitedFriends.concat(this.invitedFriendsAccepted).concat(this.invitedFriendsDeclined);
    }

    public ngOnInit(): void {
        this.friendsCtrl.valueChanges.subscribe(userName => this.filterFriends(userName));
        this.friends = this.friendsApiService.getFriends();
    }

    public assingFriendsToGroups(data: MarkerInvitation[]): void {
        if (data) {
            this.invitedFriends = data.filter(v => v.invitationStatus === InvitationStatuses.None
                || v.invitationStatus === InvitationStatuses.PendingAcceptance);
            this.invitedFriendsAccepted = data.filter(v => v.invitationStatus === InvitationStatuses.Accepted);
            this.invitedFriendsDeclined = data.filter(v => v.invitationStatus === InvitationStatuses.Declined);
        }
    }

    public acceptList(): void {
        this.dialogRef.close(this.allInvitations);
    }

    public async invite(userName: string): Promise<void> {
        const friends = await this.filtratedFriends.toPromise();
        const friend = friends.find(f => f.userName === userName);

        if (friend) {
            const invitation = new MarkerInvitation();
            invitation.invitationStatus = InvitationStatuses.PendingAcceptance;
            invitation.invitedUserFirstName = friend.firstName;
            invitation.invitedUserImage = friend.image;
            invitation.invitedUserLastName = friend.lastName;
            invitation.invitedUserUserName = friend.userName;

            this.invitedFriends.push(invitation);
            this.friendsCtrl.reset();
        } else {
            this.snackBar.open(`Cannot find friend with email ${userName}.`, 'Error!', snackbarConfiguration);
        }
    }

    public async remove(invitation: MarkerInvitation, invitationList: MarkerInvitation[]): Promise<void> {
        const result = await this.dialogService
            .confirm('Warning!', `Do you really want to remove inviation to ${invitation.invitedUserUserName}?`).toPromise();

        if (result) {
            const index = invitationList.indexOf(invitation);
            invitationList.splice(index, 1);
        }
    }

    private filterFriends(name: string): void {
        if (name) {
            this.filtratedFriends = this.friends
                .map(friend => friend.filter(f => f.userName.indexOf(name) >= 0))
                .map(friend => friend.filter(f => this.allInvitations.filter(i => i.invitedUserUserName === f.userName).length === 0));
        } else {
            this.filtratedFriends = Observable.of(new Array<Friend>());
        }
    }
}
