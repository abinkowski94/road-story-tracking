import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { ApplicationUser } from './../../shared/models/data/user/application-user.model';
import { BaseHttpService } from '../../shared/services/http-services/base-http.service';

@Injectable()
export class ManageAccountApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'ManageAccount');
    }

    public registerUser(user: ApplicationUser): Observable<ApplicationUser> {
        return this.post('UpdateUserData', user);
    }
}
