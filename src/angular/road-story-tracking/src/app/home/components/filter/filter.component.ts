import { MatSnackBar } from '@angular/material';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith,  map } from 'rxjs/operators';

import { MarkerService } from './../../services/marker.service';
import { Marker } from './../../../shared/models/data/map/marker.model';

@Component({
    templateUrl: './filter.component.html',
    styleUrls: ['./filter.component.css'],
    selector: 'app-filter'
})
export class FilterComponent {

    public markers: Marker[];
    public markerCtrl: FormControl;
    public filteredMarkers: Observable<Marker[]>;

    public constructor(private markerService: MarkerService, private snackBar: MatSnackBar) {
        this.markers = [];
        this.markerCtrl = new FormControl();
        this.filteredMarkers = this.markerCtrl.valueChanges.pipe(startWith(''),
            map(markerName => markerName ? this.filterMarkers(markerName) : this.markers.slice()));

        this.markerService.markers.subscribe((markers: Marker[]) => this.markers = markers);
    }

    public filterMarkers(name: string) {
        return this.markers.filter(marker => marker.name.toLowerCase().indexOf(name.toLowerCase()) > -1).slice(0, 10);
    }

    public turncateDescription(description: string): string {
        if (description) {
            return description.substring(0, 15);
        }
        return '';
    }
}
