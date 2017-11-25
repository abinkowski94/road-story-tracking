import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BaseHttpService } from './base-http.service';
import { TokenInfo } from '../models/data/token-info.model';


@Injectable()
export class TokenApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Auth', 'token');
    }

    public getToken(userName: string, password: string): Observable<TokenInfo> {
        const params = new HttpParams()
            .set('userName', userName)
            .set('password', password);

        return this.get(params);
    }
}
