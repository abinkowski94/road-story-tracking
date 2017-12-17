import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { environment } from 'environments/environment';

import { TokenService } from 'shared/services/user/token.service';
import { TokenInfo } from 'shared/models/data/token/token-info.model';

@Injectable()
export class AuthGuard implements CanActivate {

    public constructor(private router: Router, private tokenService: TokenService) { }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.tokenService.isAuthenticated) {
            return true;
        }
        this.router.navigate(['auth-required']);
        return false;
    }
}
