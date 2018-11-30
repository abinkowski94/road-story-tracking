import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Friend } from '../../shared/models/data/friends/friend.model';
import { Invitation } from '../../shared/models/data/friends/invitation.model';
import { BaseHttpService } from '../../shared/services/http-services/base-http.service';

@Injectable()
export class FriendsApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Contact');
    }

    public acceptInvitation(invitationId: string): Observable<Friend> {
        const params = new HttpParams().set('contactId', invitationId);
        return this.post<Friend>('AcceptInvitation', null, params);
    }

    public getFriends(): Observable<Friend[]> {
        return this.get<Friend[]>('GetMyContacts');
    }

    public getInvitations(): Observable<Invitation[]> {
        return this.get<Invitation[]>('GetIncomingInvitations');
    }

    public deleteFriend(invitationId: string): Observable<Friend> {
        const params = new HttpParams().set('contactId', invitationId);
        return this.delete('DeleteContact', params);
    }

    public getPotentionalFriens(userName: string): Observable<Friend[]> {
        const params = new HttpParams().set('userName', userName);
        return this.get<Friend[]>('GetPotentionalContacts', params);
    }

    public sendInvitation(userName: string): Observable<Friend> {
        const params = new HttpParams().set('invitedUserName', userName);
        return this.post<Friend>('SendInvitation', null, params);
    }
}
