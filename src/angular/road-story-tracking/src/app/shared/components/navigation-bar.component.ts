import { Component } from '@angular/core';

import { UserService } from './../services/user.service';

@Component({
    selector: 'app-navigation-bar',
    templateUrl: './navigation-bar.component.html',
    styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent {

    public userName: string;

    public constructor(private userService: UserService) {
        this.userService.userName.subscribe(userName => this.userName = userName);
    }
}
