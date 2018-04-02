import { DialogService } from './../../../shared/services/dialog/dialog.service';
import { Component, OnInit } from '@angular/core';

import { Friend } from '../../../shared/models/data/friends/friend.model';

@Component({
    selector: 'app-friends-list',
    templateUrl: './friends-list.component.html',
    styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit {

    public friends: Friend[];

    public constructor(private dialogService: DialogService) { }

    public ngOnInit(): void {
        this.friends = [
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
    }

    public async removeFriend(friend: Friend): Promise<void> {
        const result = await this.dialogService
            .confirm('Remove friend form list', `Do you really want to remove ${friend.firstName} ${friend.lastName} form list?`)
            .toPromise();

        if (result) {
            const index = this.friends.indexOf(friend);
            if (index > -1) {
                this.friends.splice(index, 1);
            }
        }
    }
}
