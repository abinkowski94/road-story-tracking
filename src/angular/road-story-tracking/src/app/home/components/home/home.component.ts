import { NewMarkerDialogComponent } from './../new-marker/new-marker-dialog.component';
import { MatSnackBar, MatDialog } from '@angular/material';
import { Component, AfterContentInit } from '@angular/core';
import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerService } from './../../services/marker.service';

import { MarkerServiceState } from './../../services/marker-service-state.enum';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterContentInit {

    public latitude: number;
    public longitude: number;
    public zoom: number;
    public markers: Marker[];
    public state: MarkerServiceState;

    public constructor(private snackBar: MatSnackBar, private markerService: MarkerService,
        private materialdialogService: MatDialog) {
        this.latitude = 0;
        this.longitude = 0;
        this.zoom = 15;

        this.markerService.markers.subscribe((markers: Marker[]) => {
            this.markers = markers;
        });

        this.markerService.state.subscribe((state: MarkerServiceState) => {
            this.state = state;
        });
    }

    public ngAfterContentInit(): void {
        this.getLocation();
    }

    public mapClicked($event: any): void {
        if (this.state === MarkerServiceState.AddMarker) {

            const dialogRef = this.materialdialogService.open(NewMarkerDialogComponent);

            dialogRef.afterClosed().subscribe((marker: Marker) => {
                if (marker) {
                    marker.latitude = $event.coords.lat;
                    marker.longitude = $event.coords.lng;

                    this.markerService.addMarker(marker);
                }

                this.markerService.setState(MarkerServiceState.None);
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
    }
}
