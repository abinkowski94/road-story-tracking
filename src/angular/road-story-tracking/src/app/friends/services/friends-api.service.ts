import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Friend } from '../../shared/models/data/friends/friend.model';
import { BaseHttpService } from '../../shared/services/http-services/base-http.service';

@Injectable()
export class FriendsApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Contact');
    }

    public getUserData(): Observable<Friend[]> {
        return this.get<Friend[]>('GetMyContacts');
    }

    public deleteFriend(invitationId: string): Observable<Friend> {
        const params = new HttpParams().set('contactId', invitationId);
        return this.delete('DeleteContact', params);
    }
}
