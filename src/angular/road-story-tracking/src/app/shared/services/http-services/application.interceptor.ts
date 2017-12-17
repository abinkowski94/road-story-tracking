import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { HttpHeaders, HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/do';

import { environment } from 'environments/environment';
import { TokenService } from 'shared/services/user/token.service';
import { BackendErrorResponse } from 'shared/models/responses/error-response.model';

@Injectable()
export class ApllicationInterceptor implements HttpInterceptor {

    public constructor(private router: Router, private tokenService: TokenService, private snackBar: MatSnackBar) { }

    public intercept(req: HttpRequest<any>, next: HttpHandler) {
        const newRequest = req.clone({ headers: this.getHeaders(req.headers) });
        return next.handle(newRequest).do(_ => { }, this.onErrorResponse);
    }

    private getHeaders(headers: HttpHeaders): HttpHeaders {
        if (this.tokenService.isAuthenticated) {
            headers = headers.set('Authorization', `bearer ${this.tokenService.tokenInfo.token}`);
        }

        return headers.set('Content-Type', 'application/json');
    }

    private onErrorResponse = (error: HttpErrorResponse) => {
        if (error.status === 401) {
            this.router.navigate(['auth-required']);
        } else if (error.status === 400) {
            this.snackBar.open((error.error as BackendErrorResponse).exception.Message, 'Error!',
                { duration: 3000, horizontalPosition: 'right' });
        } else if (error.status === 404) {
            this.router.navigate(['not-found']);
        }
    }
}


