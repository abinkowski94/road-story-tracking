import { MatSnackBar } from '@angular/material';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ManageAccountApiService } from './../../services/manage-account-api.service';
import { ApplicationUser } from '../../../shared/models/data/user/application-user.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';

@Component({
  templateUrl: './manage-account.component.html',
  styleUrls: ['./manage-account.component.css']
})
export class ManageAccountComponent implements OnInit {

  public applicationUser: ApplicationUser;
  public pendingRequest: boolean;

  public constructor(private manageAccountApiService: ManageAccountApiService, private snackBar: MatSnackBar) {
    this.applicationUser = new ApplicationUser();
  }

  public async ngOnInit(): Promise<void> {
    try {
      this.applicationUser = await this.manageAccountApiService.getUserData().toPromise();
    } catch (error) {
      this.snackBar.open((error.error as BackendErrorResponse).exception.Message, 'Error!', {
        duration: 3000,
        horizontalPosition: 'right'
      });
    }
  }

  public updateUserInfo(): void {
    this.pendingRequest = true;
    this.manageAccountApiService.updateUserData(this.applicationUser)
      .subscribe((response: ApplicationUser) => {
        this.applicationUser = response;
        this.snackBar.open('Data has been updated successfully.', 'Success!', {
          duration: 3000,
          horizontalPosition: 'right'
        });
      }, (error: HttpErrorResponse) => {
        this.pendingRequest = false;
        this.snackBar.open((error.error as BackendErrorResponse).exception.Message, 'Error!', {
          duration: 3000,
          horizontalPosition: 'right'
        });
      }, () => this.pendingRequest = false);
  }
}
