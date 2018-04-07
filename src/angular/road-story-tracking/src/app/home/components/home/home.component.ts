import { MatSnackBar, MatDialog } from '@angular/material';
import { Component, OnInit, OnDestroy } from '@angular/core';

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
export class HomeComponent implements OnInit, OnDestroy {

    public state: MarkerServiceState;

    public mapLatitude: number;
    public mapLongitude: number;
    public latitude: number;
    public longitude: number;
    public markers: Marker[];
    public zoom = 15;
    public geolocationWatchId?: number;

    public constructor(private snackBar: MatSnackBar, private markerService: MarkerService,
        private materialdialogService: MatDialog) { }

    public ngOnInit(): void {
        this.getLocation();
        this.trackPosition();
        this.markerService.getMarkers();
        this.markerService.markers.subscribe((markers: Marker[]) => this.markers = markers);
        this.markerService.state.subscribe((state: MarkerServiceState) => this.state = state);
    }

    public ngOnDestroy(): void {
        if (navigator.geolocation && this.geolocationWatchId) {
            navigator.geolocation.clearWatch(this.geolocationWatchId);
        }
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
        this.readPositionsFromCache();
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.mapLatitude = position.coords.latitude;
                this.mapLongitude = position.coords.longitude;
                this.savePositionsIntoCache();
            }, (error: any) => {
                this.snackBar.open('Browser does not allow to get your location.', 'Error!', snackbarConfiguration);
            });
        } else {
            this.snackBar.open('Browser does not allow to get your location.', 'Error!', snackbarConfiguration);
        }
    }

    public trackPosition(): void {
        if (navigator.geolocation) {
            this.geolocationWatchId = navigator.geolocation.watchPosition((position: Position) => {
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
        this.markerService.getMarkers();
        this.zoom = 15;
    }

    public translateTypeToIcon(type: MarkerType): string {
        return this.markerService.translateTypeToIcon(type);
    }

    private savePositionsIntoCache() {
        localStorage.setItem('CityStoryTracking.mapLatitude', this.mapLatitude.toString());
        localStorage.setItem('CityStoryTracking.mapLongitude', this.mapLongitude.toString());
    }

    private readPositionsFromCache() {
        if (localStorage.getItem) {
            this.mapLatitude = parseFloat(localStorage.getItem('CityStoryTracking.mapLatitude') || '0');
            this.mapLongitude = parseFloat(localStorage.getItem('CityStoryTracking.mapLongitude') || '0');
        }
    }
}
