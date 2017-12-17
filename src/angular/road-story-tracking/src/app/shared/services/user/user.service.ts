import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/do';

import { TokenService } from 'shared/services/user/token.service';
import { UserApiService } from 'shared/services/user/user-api.service';
import { environment } from 'environments/environment';
import { TokenInfo } from 'shared/models/data/token/token-info.model';
import { ApplicationUser } from 'shared/models/data/user/application-user.model';
import { RegisterUser } from 'shared/models/data/user/register-user.model';

@Injectable()
export class UserService {

    private _tokenInfo: BehaviorSubject<TokenInfo>;
    public readonly tokenInfo: Observable<TokenInfo>;

    public constructor(private userApiService: UserApiService, private tokenService: TokenService) {
        this._tokenInfo = new BehaviorSubject<TokenInfo>(this.tokenService.tokenInfo);
        this.tokenInfo = this._tokenInfo.asObservable();
    }

    public get userName(): Observable<string> {
        return this.isAuthenticated.map((authenticated: boolean) => {
            return authenticated ? this._tokenInfo.getValue().userName : '';
        });
    }

    public get isAuthenticated(): Observable<boolean> {
        return this.tokenInfo.map(token => this.tokenService.isAuthenticated);
    }

    public login(userName: string, password: string): Observable<TokenInfo> {
        return this.userApiService.getToken(userName, password)
            .do((tokenInfo: TokenInfo) => {
                this.tokenService.saveToken(tokenInfo);
                this._tokenInfo.next(tokenInfo);
            });
    }

    public logOff(): void {
        this.tokenService.clearToken();
        this._tokenInfo.next(null);
    }

    public register(user: RegisterUser): Observable<ApplicationUser> {
        return this.userApiService.registerUser(user);
    }
}
