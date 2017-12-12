import { MatSnackBar, MatDialog } from '@angular/material';
import { Component, AfterContentInit } from '@angular/core';

import { MarkerService } from './../../services/marker.service';
import { NewMarkerDialogComponent } from './../new-marker/new-marker-dialog.component';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerServiceState } from './../../services/marker-service-state.enum';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterContentInit {

    public state: MarkerServiceState;

    public latitude: number;
    public longitude: number;
    public zoom: number;
    public markers: Marker[];

    public constructor(private snackBar: MatSnackBar, private markerService: MarkerService, private materialdialogService: MatDialog) {
        this.latitude = 0;
        this.longitude = 0;
        this.zoom = 15;

        this.markerService.getMarkers();
        this.markerService.markers.subscribe((markers: Marker[]) => this.markers = markers);
        this.markerService.state.subscribe((state: MarkerServiceState) => this.state = state);
    }

    public ngAfterContentInit(): void {
        this.getLocation();
    }

    public mapClicked($event: any): void {
        if (this.state === MarkerServiceState.AddMarker) {
            this.materialdialogService.open(NewMarkerDialogComponent, {
                data: {
                    latitude: $event.coords.lat,
                    longitude: $event.coords.lng
                }
            });
        }
    }

    public getLocation(): void {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
            }, (error: any) => {
                this.snackBar.open('Browser does not allow to get your location.', 'Error!', {
                    duration: 3000,
                    horizontalPosition: 'right'
                });
            });
        } else {
            this.snackBar.open('Browser does not allow to get your location.', 'Error!', {
                duration: 3000,
                horizontalPosition: 'right'
            });
        }
    }

    public refreshMap(): void {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
            });
        }
        this.zoom = 15;
        this.markerService.getMarkers();
    }
}
