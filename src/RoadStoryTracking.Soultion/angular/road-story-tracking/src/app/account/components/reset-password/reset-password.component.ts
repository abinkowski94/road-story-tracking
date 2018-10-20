import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';

import { ManageAccountApiService } from '../../services/manage-account-api.service';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';
import { BackendErrorResponse } from '../../../shared/models/responses/error-response.model';
import { CustomAggregatedBackendException } from '../../../shared/models/exceptions/custom-aggregated-backend-exception';

@Component({
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

    public userName: string;
    public token: string;
    public password: string;
    public confirmPassword: string;
    public errors: string[];

    public constructor(private manageAccountApiService: ManageAccountApiService, private snackBar: MatSnackBar,
        private router: Router) { }

    public ngOnInit(): void {
        const url = new URL(window.location.href);
        this.token = url.searchParams.get('token');
        this.userName = url.searchParams.get('userName');
    }

    public async resetPassword(): Promise<void> {
        if (this.confirmPassword !== this.password) {
            this.errors = ['Passwords do not match!'];
        } else {
            try {
                await this.manageAccountApiService.resetPassword(this.userName, this.token, this.password).toPromise();
                this.snackBar.open('Password has been reseted.', 'Success!', snackbarConfiguration);
                setTimeout(() => this.router.navigate(['account/login']), snackbarConfiguration.duration + 1000);
            } catch (error) {
                const exceptions = ((error.error as BackendErrorResponse).exception as CustomAggregatedBackendException).exceptions;
                this.errors = exceptions.map(e => e.message);
                const errorMessage = (error.error as BackendErrorResponse).exception.message;
                this.snackBar.open(errorMessage, 'Error!', snackbarConfiguration);
            }
        }
    }
}
