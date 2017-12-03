import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BaseHttpService } from './../http-services/base-http.service';
import { TokenInfo } from './../../models/data/token/token-info.model';
import { ApplicationUser } from './../../models/data/user/application-user.model';
import { RegisterUser } from './../../models/data/user/register-user.model';

@Injectable()
export class UserApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Auth');
    }

    public getToken(userName: string, password: string): Observable<TokenInfo> {
        const params = new HttpParams().set('userName', userName).set('password', password);
        return this.get('token', params);
    }

    public registerUser(user: RegisterUser): Observable<ApplicationUser> {
        return this.post('register', user);
    }
}
