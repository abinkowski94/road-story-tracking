import { DialogService } from './../../../shared/services/dialog/dialog.service';
import { Component, OnInit } from '@angular/core';

import { FriendsApiService } from '../../services/friends-api.service';
import { Friend } from '../../../shared/models/data/friends/friend.model';

@Component({
    selector: 'app-friends-list',
    templateUrl: './friends-list.component.html',
    styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit {

    public friends: Friend[];

    public constructor(private dialogService: DialogService, private friendsApiService: FriendsApiService) { }

    public ngOnInit(): void {
        this.friendsApiService.getFriends().subscribe(friends => this.friends = friends);
    }

    public async removeFriend(friend: Friend): Promise<void> {
        const fullName = friend.firstName || friend.lastName ? `${friend.firstName} ${friend.lastName}` : friend.userName;
        const result = await this.dialogService
            .confirm('Remove friend form list', `Do you really want to remove ${fullName} form list?`)
            .toPromise();

        if (result) {
            const apiResponse = await this.friendsApiService.deleteFriend(friend.invitationId).toPromise();

            const index = this.friends.indexOf(friend);
            if (index > -1) {
                this.friends.splice(index, 1);
            }
        }
    }
}
