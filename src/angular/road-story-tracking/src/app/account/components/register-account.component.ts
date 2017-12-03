import { Component, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { RegisterUser } from './../../shared/models/data/user/register-user.model';
import { UserService } from './../../shared/services/user.service';
import { ApplicationUser } from '../../shared/models/data/user/application-user.model';
import { BackendErrorResponse } from './../../shared/models/responses/error-response.model';
import { CustomAggregatedBackendException } from './../../shared/models/exceptions/custom-aggregated-backend-exception';


@Component({
    templateUrl: './register-account.component.html',
    styleUrls: ['./register-account.component.css']
})
export class RegisterAccountComponent {

    @ViewChild('registerForm') registerForm: FormGroup;

    public readonly registerUserModel: RegisterUser;
    public errors: string[];
    public pendingRequest: boolean;

    public constructor(private userService: UserService, private router: Router) {
        this.registerUserModel = new RegisterUser();
        this.errors = [];
    }

    public register(): void {
        if (this.registerUserModel.password !== this.registerUserModel.confirmPassword) {
            this.errors = ['Passwords do not match!'];
        } else {
            this.pendingRequest = true;
            this.userService.register(this.registerUserModel)
                .subscribe((result: ApplicationUser) => {
                    this.router.navigate(['account/register/complete']);
                }, (error: HttpErrorResponse) => {
                    this.pendingRequest = false;
                    const exceptions = ((error.error as BackendErrorResponse).exception as CustomAggregatedBackendException)
                        .Exceptions;
                    this.errors = exceptions.map(e => e.Message);
                });
        }
    }
}
