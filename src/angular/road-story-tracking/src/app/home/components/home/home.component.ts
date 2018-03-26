import { MatSnackBar, MatDialog } from '@angular/material';
import { Component, AfterContentInit, OnInit } from '@angular/core';

import { MarkerService } from './../../services/marker.service';
import { NewMarkerDialogComponent } from './../new-marker/new-marker-dialog.component';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerType } from './../../../shared/models/data/map/marker-type.enum.model';
import { MarkerServiceState } from './../../services/marker-service-state.enum';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterContentInit {

    public state: MarkerServiceState;

    public latitude: number;
    public longitude: number;
    public zoom: number;
    public markers: Marker[];

    public constructor(private snackBar: MatSnackBar, private markerService: MarkerService, private materialdialogService: MatDialog) {
        this.latitude = 0;
        this.longitude = 0;
        this.zoom = 15;
    }

    public ngOnInit(): void {
        this.markerService.getMarkers();
        this.markerService.markers.subscribe((markers: Marker[]) => this.markers = markers);
        this.markerService.state.subscribe((state: MarkerServiceState) => this.state = state);
    }

    public ngAfterContentInit(): void {
        this.getLocation();
    }

    public async mapClicked($event: any): Promise<void> {
        if (this.state === MarkerServiceState.AddMarker) {
            const dialogData = {
                data: {
                    latitude: $event.coords.lat,
                    longitude: $event.coords.lng
                }
            };

            await this.materialdialogService.open(NewMarkerDialogComponent, dialogData).afterClosed().toPromise();
            this.markerService.setState(MarkerServiceState.None);
        }
    }

    public getLocation(): void {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
            }, (error: any) => {
                this.snackBar.open('Browser does not allow to get your location.', 'Error!', snackbarConfiguration);
            });
        } else {
            this.snackBar.open('Browser does not allow to get your location.', 'Error!', snackbarConfiguration);
        }
    }

    public refreshMap(): void {
        this.getLocation();
        this.zoom = 15;
        this.markerService.getMarkers();
    }

    public translateTypeToIcon(type: MarkerType): string {
        return this.markerService.translateTypeToIcon(type);
    }
}
