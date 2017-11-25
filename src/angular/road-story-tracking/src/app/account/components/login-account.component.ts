import { Component } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { UserService } from './../../shared/services/user.service';
import { TokenInfo } from '../../shared/models/data/token-info.model';
import { CustomResponse } from '../../shared/models/responses/custom-response.model';

@Component({
    templateUrl: './login-account.component.html'
})
export class LoginAccountComponent {

    public loginInput = '';
    public passwordInput = '';

    public constructor(private userService: UserService, private router: Router) { }

    public login() {
        this.userService.login(this.loginInput, this.passwordInput)
            .subscribe((result: TokenInfo) => {
                this.router.navigate(['account/manage']);
            }, (error: any) => {
                alert((error.error as CustomResponse<TokenInfo>).exception.Message);
            });
    }
}
