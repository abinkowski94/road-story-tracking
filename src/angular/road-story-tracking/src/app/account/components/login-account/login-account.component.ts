import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { UserService } from './../../../shared/services/user/user.service';
import { TokenInfo } from './../../../shared/models/data/token/token-info.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';

@Component({
    templateUrl: './login-account.component.html',
    styleUrls: ['./login-account.component.css']
})
export class LoginAccountComponent {

    public pendingRequest: boolean;
    public userLogin: string;
    public password: string;

    public constructor(private userService: UserService, private router: Router, private snackBar: MatSnackBar) { }

    public login() {
        this.pendingRequest = true;

        this.userService.login(this.userLogin, this.password)
            .subscribe((result: TokenInfo) => {
                this.router.navigate(['home']);
            }, (error: HttpErrorResponse) => {
                this.pendingRequest = false;
                this.snackBar.open((error.error as BackendErrorResponse).exception.Message, 'Error!', {
                    duration: 3000,
                    horizontalPosition: 'right'
                });
            }, () => this.pendingRequest = false);
    }
}
