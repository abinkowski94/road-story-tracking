import { MatSnackBar } from '@angular/material';
import { Component, OnInit } from '@angular/core';
import { Marker } from './../../../shared/models/data/map/marker.model';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    public latitude = 0;
    public longitude = 0;
    public zoom = 15;
    public markers: Marker[];

    public constructor(private snackBar: MatSnackBar) {
        this.markers = [];
    }

    public ngOnInit(): void {
        this.getLocation();
    }

    private getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;

                const marker = new Marker();
                marker.latitude = this.latitude;
                marker.longitude = this.longitude;
                marker.iconUrl = 'assets/icons/my-position-marker.png';

                this.markers.push(marker);
            });
        } else {
            this.snackBar.open('Browser does not allow to get your location.', 'Error!', {
                duration: 3000,
                horizontalPosition: 'right'
            });
        }
    }


}
