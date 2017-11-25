import { Injectable } from '@angular/core';
import { HttpHeaders, HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import 'rxjs/add/operator/do';

import { environment } from './../../../environments/environment';
import { TokenInfo } from './../models/data/token-info.model';

@Injectable()
export class ApllicationInterceptor implements HttpInterceptor {

    public constructor(private router: Router) { }

    public intercept(req: HttpRequest<any>, next: HttpHandler) {
        const newRequest = req.clone({
            headers: this.getHeaders(req.headers)
        });

        return next.handle(newRequest)
            .do(_ => { },
            (error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.router.navigate(['auth-required']);
                }
            });
    }

    private getHeaders(headers: HttpHeaders): HttpHeaders {
        const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem(environment.localStorageTokenKey));

        if (tokenInfo && tokenInfo.expirationDate) {
            tokenInfo.expirationDate = new Date(tokenInfo.expirationDate);
        }

        if (tokenInfo
            && tokenInfo.token
            && tokenInfo.expirationDate
            && tokenInfo.userName
            && tokenInfo.expirationDate.getTime
            && tokenInfo.expirationDate.getTime() > Date.now()) {
            return headers
                .set('Content-Type', 'application/json')
                .set('Authorization', `bearer ${tokenInfo.token}`);
        }

        return headers
            .set('Content-Type', 'application/json');
    }
}


