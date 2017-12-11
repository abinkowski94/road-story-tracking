import { Component } from '@angular/core';
import { MatSnackBar, MatTableDataSource } from '@angular/material';

import { MarkerApiService } from './../../services/marker-api.service';
import { Marker } from '../../../shared/models/data/map/marker.model';
import { MarkerType } from '../../../shared/models/data/map/marker-type.enum.model';

@Component({
    templateUrl: 'my-markers.component.html',
    styleUrls: ['my-markers.component.css'],
})
export class MyMarkersComponent {

    public displayedColumns: string[];
    public displayedColumnsMobile: string[];
    public markersData: MatTableDataSource<Marker>;

    public constructor(private snackBar: MatSnackBar, private markerApiService: MarkerApiService) {
        this.markersData = new MatTableDataSource<Marker>();
        this.displayedColumns = ['name', 'type', 'created', 'actions'];
        this.displayedColumnsMobile = ['name', 'actions'];

        this.markerApiService.getMyMarkers().subscribe((result: Marker[]) => {
            this.markersData = new MatTableDataSource<Marker>(result);
        });
    }

    public modify(marker: Marker): void {
        // TODO
    }

    public delete(marker: Marker): void {
        // TODO
    }

    public getTypeName(type: MarkerType): string {
        if (type === MarkerType.CashRelated) {
            return 'Cash related';
        } else if (type === MarkerType.NeedARide) {
            return 'Need a ride';
        } else if (type === MarkerType.Party) {
            return 'Party';
        } else {
            return 'Other';
        }
    }

    public get isMobile(): boolean {
        return window.screen.width < 768;
    }
}
