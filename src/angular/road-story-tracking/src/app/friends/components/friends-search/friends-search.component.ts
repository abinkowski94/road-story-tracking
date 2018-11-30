import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';

import { FriendsApiService } from '../../services/friends-api.service';
import { DialogService } from '../../../shared/services/dialog/dialog.service';
import { Friend } from '../../../shared/models/data/friends/friend.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    selector: 'app-friends-search',
    templateUrl: './friends-search.component.html',
    styleUrls: ['./friends-search.component.css']
})
export class FriendsSearchComponent implements OnInit {

    public potentionalFriends: Friend[];
    public friendsCtrl: FormControl;
    public filtratedFriends: Observable<Friend[]>;

    public constructor(private dialogService: DialogService, private snackBar: MatSnackBar, private friendsApiService: FriendsApiService) {
        this.friendsCtrl = new FormControl();
    }

    public ngOnInit(): void {
        this.potentionalFriends = [];

        this.friendsCtrl.valueChanges.subscribe(userName => this.filterFriends(userName));
    }

    public async sendInvitation(userEmail: string): Promise<void> {
        const result = await this.dialogService
            .confirm('Send invitation', `Do you want to send invitation to ${userEmail}?`).toPromise();

        if (result) {
            try {
                await this.friendsApiService.sendInvitation(userEmail).toPromise();
            } catch (exception) { }

            this.snackBar.open(`The invitation has been sent to ${userEmail}.`, 'Success!', snackbarConfiguration);
            this.friendsCtrl.reset();
        }
    }

    private filterFriends(name: string): void {
        if (name && name.length > 3) {
            this.filtratedFriends = this.friendsApiService.getPotentionalFriens(name);
        } else {
            this.filtratedFriends = Observable.of(new Array<Friend>());
        }
    }
}
