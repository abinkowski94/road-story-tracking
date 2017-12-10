import { MatSnackBar } from '@angular/material';
import { Component, Output, EventEmitter } from '@angular/core';

import { MarkerService } from './../../services/marker.service';
import { MarkerServiceState } from '../../services/marker-service-state.enum';
import { UserService } from './../../../shared/services/user/user.service';

@Component({
    templateUrl: './navigation-menu.component.html',
    styleUrls: ['./navigation-menu.component.css'],
    selector: 'app-navigation-menu'
})
export class NavigationMenuComponent {

    @Output() public refreshMapClick: EventEmitter<any>;
    public state: MarkerServiceState;
    public isAuthenticated: boolean;

    public constructor(private markerService: MarkerService, private snackBar: MatSnackBar, private userService: UserService) {
        this.refreshMapClick = new EventEmitter<any>();
        this.markerService.state.subscribe((state: MarkerServiceState) => this.state = state);
        this.userService.isAuthenticated.subscribe((result: boolean) => this.isAuthenticated = result);
    }

    public setMarkerMode(mode: MarkerServiceState): void {
        if (this.state !== mode) {
            this.markerService.setState(mode);
        } else {
            this.markerService.setState(MarkerServiceState.None);
        }
    }

    public refreshMap(): void {
        this.refreshMapClick.emit('');
    }
}
