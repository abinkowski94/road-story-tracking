import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators/map';
import { startWith } from 'rxjs/operators/startWith';

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

    public constructor(private dialogService: DialogService, private snackBar: MatSnackBar) {
        this.friendsCtrl = new FormControl();
    }

    public ngOnInit(): void {
        this.potentionalFriends = [
            {
                firstName: 'John',
                lastName: 'Doe',
                userName: 'john.doe@test.ts',
                image: null
            },
            {
                firstName: 'Janusz',
                lastName: 'Kowalski',
                userName: 'janusz.kowalski@test.ts',
                image: null
            }
        ];

        this.filtratedFriends = this.friendsCtrl.valueChanges.pipe(startWith(''),
            map(userName => userName && userName.length > 3 ? this.filterFriends(userName) : []));
    }

    public async sendInvitation(userEmail: string): Promise<void> {
        const result = await this.dialogService
            .confirm('Send invitation', `Do you want to send invitation to ${userEmail}?`).toPromise();

        if (result) {
            this.friendsCtrl.reset();
            this.snackBar.open(`The invitation has been sent to ${userEmail}.`, 'Success!', snackbarConfiguration);
        }
    }

    private filterFriends(name: string): Friend[] {
        return this.potentionalFriends.filter(friend => friend.userName.toLowerCase().indexOf(name.toLowerCase()) > -1).slice(0, 10);
    }
}
