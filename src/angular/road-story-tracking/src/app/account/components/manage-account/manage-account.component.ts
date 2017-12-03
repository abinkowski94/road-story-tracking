import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { ManageAccountApiService } from './../../services/manage-account-api.service';
import { ApplicationUser } from '../../../shared/models/data/user/application-user.model';

@Component({
    templateUrl: './manage-account.component.html',
    styleUrls: ['./manage-account.component.css']
})
export class ManageAccountComponent implements OnInit {

    public applicationUser: ApplicationUser;
    public pendingRequest: boolean;

    public constructor(private manageAccountApiService: ManageAccountApiService) {
        this.applicationUser = new ApplicationUser();
    }

    public ngOnInit(): void {
        this.manageAccountApiService.getUserData()
            .subscribe((user: ApplicationUser) => {
                this.applicationUser = user;
            },
            (error: HttpErrorResponse) => {

            });
    }
}
