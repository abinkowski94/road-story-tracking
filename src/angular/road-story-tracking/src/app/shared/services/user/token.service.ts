import { Injectable } from '@angular/core';

import { environment } from 'environments/environment';
import { TokenInfo } from 'shared/models/data/token/token-info.model';

@Injectable()
export class TokenService {

    public get tokenInfo(): TokenInfo {
        const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem(environment.localStorageTokenKey));

        if (tokenInfo && tokenInfo.expirationDate) {
            tokenInfo.expirationDate = new Date(tokenInfo.expirationDate);
        }

        return tokenInfo;
    }

    public get isAuthenticated(): boolean {
        return this.tokenInfo
            && this.tokenInfo.token
            && this.tokenInfo.expirationDate
            && this.tokenInfo.userName
            && this.tokenInfo.expirationDate.getTime
            && this.tokenInfo.expirationDate.getTime() > Date.now();
    }

    public saveToken(tokenInfo: TokenInfo): TokenInfo {
        localStorage.setItem(environment.localStorageTokenKey, JSON.stringify(tokenInfo));
        return tokenInfo;
    }

    public clearToken(): void {
        localStorage.setItem(environment.localStorageTokenKey, null);
    }
}
