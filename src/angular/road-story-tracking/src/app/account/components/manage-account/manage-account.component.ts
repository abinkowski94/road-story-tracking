import { MatSnackBar } from '@angular/material';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ManageAccountApiService } from './../../services/manage-account-api.service';
import { ApplicationUser } from '../../../shared/models/data/user/application-user.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: './manage-account.component.html',
    styleUrls: ['./manage-account.component.css']
})
export class ManageAccountComponent implements OnInit {

    public applicationUser: ApplicationUser;
    public pendingRequest: boolean;

    public constructor(private manageAccountApiService: ManageAccountApiService, private snackBar: MatSnackBar) { }

    public ngOnInit(): void {
        this.manageAccountApiService.getUserData().subscribe(applicationUser => {
            this.applicationUser = applicationUser;
        }, error => this.showError(error));
    }

    public async updateUserInfo(): Promise<void> {
        this.pendingRequest = true;

        try {
            this.applicationUser = await this.manageAccountApiService.updateUserData(this.applicationUser).toPromise();
            this.snackBar.open('Data has been updated successfully.', 'Success!', snackbarConfiguration);
        } catch (error) {
            this.showError(error);
        }

        this.pendingRequest = false;
    }

    private showError = (error: BackendErrorResponse) => {
        if (error && error.exception && error.exception.message) {
            this.snackBar.open(error.exception.message, 'Error!', snackbarConfiguration);
        }
    }
}
