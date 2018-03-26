import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar, MatTableDataSource, MatDialog } from '@angular/material';

import { DialogService } from './../../../shared/services/dialog/dialog.service';
import { MarkerApiService } from './../../services/marker-api.service';
import { UpdateMarkerDialogComponent } from './../modify-marker/update-marker-dialog.component';
import { Marker } from '../../../shared/models/data/map/marker.model';
import { MarkerType } from '../../../shared/models/data/map/marker-type.enum.model';
import { BackendErrorResponse } from '../../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: 'my-markers.component.html',
    styleUrls: ['my-markers.component.css'],
})
export class MyMarkersComponent {

    public displayedColumns: string[];
    public displayedColumnsMobile: string[];
    public markersData: MatTableDataSource<Marker>;

    public constructor(private snackBar: MatSnackBar, private markerApiService: MarkerApiService, private dialogService: DialogService,
        private materialdialogService: MatDialog) {
        this.markersData = new MatTableDataSource<Marker>();
        this.displayedColumns = ['name', 'type', 'created', 'actions'];
        this.displayedColumnsMobile = ['name', 'actions'];

        this.markerApiService.getMyMarkers().subscribe((result: Marker[]) => {
            this.markersData = new MatTableDataSource<Marker>(result);
        });
    }

    public modify(marker: Marker): void {
        this.materialdialogService.open(UpdateMarkerDialogComponent, {
            data: marker
        }).afterClosed().subscribe((updatedMarker: Marker) => {
            marker = updatedMarker;
        });
    }

    public delete(marker: Marker): void {
        this.dialogService.confirm('Delete event', `Do you really want to delete this event called: '${marker.name}'?`)
            .subscribe((result: boolean) => {
                if (result === true) {
                    this.markerApiService.deleteMarker(marker.id).subscribe((deletedMarker: Marker) => {

                        const data = this.markersData.data;
                        data.splice(data.indexOf(marker), 1);
                        this.markersData = new MatTableDataSource(data);

                        this.snackBar.open(`Event ${marker.name} has been deleted`, 'Delete success', {
                            duration: 3000,
                            horizontalPosition: 'right'
                        });

                    }, (error: HttpErrorResponse) => {
                        this.snackBar.open((error.error as BackendErrorResponse).exception.message, 'Error', snackbarConfiguration);
                    });
                }
            });
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
