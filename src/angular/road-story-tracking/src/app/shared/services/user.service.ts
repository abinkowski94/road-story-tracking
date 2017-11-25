import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';

import { TokenApiService } from './token-api.service';
import { CustomResponse } from './../models/responses/custom-response.model';
import { environment } from './../../../environments/environment';
import { TokenInfo } from './../models/data/token-info.model';

@Injectable()
export class UserService {

    public constructor(private tokenApiService: TokenApiService) { }

    public get userName(): string {
        const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem(environment.localStorageTokenKey));
        return this.isAuthenticated ? tokenInfo.userName : '';
    }

    public get isAuthenticated(): boolean {
        const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem(environment.localStorageTokenKey));

        if (tokenInfo && tokenInfo.expirationDate) {
            tokenInfo.expirationDate = new Date(tokenInfo.expirationDate);
        }
        return tokenInfo
            && tokenInfo.token
            && tokenInfo.expirationDate
            && tokenInfo.userName
            && tokenInfo.expirationDate.getTime
            && tokenInfo.expirationDate.getTime() > Date.now();
    }

    public login(userName: string, password: string): Observable<TokenInfo> {
        return this.tokenApiService.getToken(userName, password)
            .do((tokenInfo: TokenInfo) => {
                localStorage.setItem(environment.localStorageTokenKey, JSON.stringify(tokenInfo));
            });
    }
}
