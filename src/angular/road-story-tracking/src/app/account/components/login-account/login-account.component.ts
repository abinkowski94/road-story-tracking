import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';

import { UserService } from '../../../shared/services/user/user.service';
import { TokenInfo } from '../../../shared/models/data/token/token-info.model';
import { BackendErrorResponse } from '../../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';
import { DialogService } from '../../../shared/services/dialog/dialog.service';
import { UserApiService } from '../../../shared/services/user/user-api.service';

@Component({
    templateUrl: 'login-account.component.html',
    styleUrls: ['login-account.component.css']
})
export class LoginAccountComponent {

    public pendingRequest: boolean;
    public userLogin: string;
    public password: string;

    public constructor(private userService: UserService, private router: Router, private snackBar: MatSnackBar,
        private dialogService: DialogService, private userApiService: UserApiService) { }

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

    public async forgotPassword(): Promise<void> {
        const result = await this.dialogService.inputText('Password reset!', 'Please enter your email').toPromise();
        if (result) {
            try {
                const apiResult = await this.userApiService.resetPassword(result).toPromise();
                this.snackBar.open(`Email with reset link has been send to ${result}.`, 'Sucess!', snackbarConfiguration);
            } catch (exception) { }
        }
    }
}
