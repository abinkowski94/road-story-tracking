import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../../../shared/services/user/user.service';
import { TokenInfo } from '../../../shared/models/data/token/token-info.model';
import { BackendErrorResponse } from '../../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: 'login-account.component.html',
    styleUrls: ['login-account.component.css']
})
export class LoginAccountComponent {

    public pendingRequest: boolean;
    public userLogin: string;
    public password: string;

    public constructor(private userService: UserService, private router: Router, private snackBar: MatSnackBar) { }

    public async login(): Promise<void> {
        this.pendingRequest = true;

        try {
            await this.userService.login(this.userLogin, this.password).toPromise();
            this.router.navigate(['home']);
        } catch (error) {
            const errorMessage = (error.error as BackendErrorResponse).exception.message;
            this.snackBar.open(errorMessage, 'Error!', snackbarConfiguration);
        }

        this.pendingRequest = false;
    }
}
