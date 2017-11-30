import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    public title = 'My first AGM project';
    public latitude = 0;
    public longitude = 0;

    public ngOnInit(): void {
        this.getLocation();
    }

    private getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
            });
        } else {

        }
    }
}
