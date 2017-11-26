import { Component } from '@angular/core';

import { UserService } from './../services/user.service';

@Component({
    selector: 'app-navigation-bar',
    templateUrl: './navigation-bar.component.html',
    styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent {

    public userName: string;
    public isAuthenticated: boolean;

    public constructor(private userService: UserService) {
        this.userName = '';
        this.isAuthenticated = false;

        this.userService.userName.subscribe(userName => this.userName = userName);
        this.userService.isAuthenticated.subscribe(isAuthenticated => this.isAuthenticated = isAuthenticated);
    }

    public logOff(): void {
        this.userService.logOff();
        window.location.reload();
    }
}
