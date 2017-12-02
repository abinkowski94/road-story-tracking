import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/do';

import { UserApiService } from './user-api.service';
import { environment } from './../../../environments/environment';
import { TokenInfo } from './../models/data/token-info.model';
import { ApplicationUser } from './../models/data/user/application-user.model';
import { RegisterUser } from './../models/data/user/register-user.model';

@Injectable()
export class UserService {

    private _tokenInfo: BehaviorSubject<TokenInfo>;
    public readonly tokenInfo: Observable<TokenInfo>;

    public constructor(private userApiService: UserApiService) {
        this._tokenInfo = new BehaviorSubject<TokenInfo>(this.getTokenInfo());
        this.tokenInfo = this._tokenInfo.asObservable();
    }

    public get userName(): Observable<string> {
        return this.isAuthenticated.map((authenticated: boolean) => {
            return authenticated ? this._tokenInfo.getValue().userName : '';
        });
    }

    public get isAuthenticated(): Observable<boolean> {
        return this.tokenInfo.map((tokenInfo: TokenInfo) => {

            if (tokenInfo && tokenInfo.expirationDate) {
                tokenInfo.expirationDate = new Date(tokenInfo.expirationDate);
            }

            return tokenInfo
                && tokenInfo.token
                && tokenInfo.expirationDate
                && tokenInfo.userName
                && tokenInfo.expirationDate.getTime
                && tokenInfo.expirationDate.getTime() > Date.now();
        });
    }

    public login(userName: string, password: string): Observable<TokenInfo> {
        return this.userApiService.getToken(userName, password)
            .do((tokenInfo: TokenInfo) => {
                localStorage.setItem(environment.localStorageTokenKey, JSON.stringify(tokenInfo));
                this._tokenInfo.next(tokenInfo);
            });
    }

    public logOff(): void {
        localStorage.setItem(environment.localStorageTokenKey, null);
        this._tokenInfo.next(null);
    }

    public register(user: RegisterUser): Observable<ApplicationUser> {
        return this.userApiService.registerUser(user);
    }

    private getTokenInfo(): TokenInfo {
        const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem(environment.localStorageTokenKey));

        if (tokenInfo && tokenInfo.expirationDate) {
            tokenInfo.expirationDate = new Date(tokenInfo.expirationDate);
        }

        return tokenInfo;
    }
}
