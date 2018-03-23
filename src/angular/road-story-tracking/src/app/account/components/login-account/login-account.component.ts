import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../../../shared/services/user/user.service';
import { TokenInfo } from '../../../shared/models/data/token/token-info.model';
import { BackendErrorResponse } from '../../../shared/models/responses/error-response.model';

@Component({
    templateUrl: 'login-account.component.html',
    styleUrls: ['login-account.component.css']
})
export class LoginAccountComponent {

    public pendingRequest: boolean;
    public userLogin: string;
    public password: string;

    public constructor(private userService: UserService, private router: Router) { }

    public login() {
        this.pendingRequest = true;

        this.userService.login(this.userLogin, this.password)
            .subscribe((result: TokenInfo) => {
                this.router.navigate(['home']);
                this.pendingRequest = false;
            },
            error => this.pendingRequest = false);
    }
}
