import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { UserService } from './../../shared/services/user.service';
import { environment } from './../../../environments/environment';
import { TokenInfo } from '../../shared/models/data/token-info.model';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.userService.isAuthenticated.map((isAuthenticated: boolean) => {
            if (isAuthenticated) {
                return true;
            }
            this.router.navigate(['auth-required']);
            return false;
        });
    }
}
