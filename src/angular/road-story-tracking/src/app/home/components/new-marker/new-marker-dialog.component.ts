import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Marker } from './../../../shared/models/data/map/marker.model';

@Component({
    templateUrl: 'new-marker-dialog.component.html',
    styleUrls: ['new-marker-dialog.component.css'],
})
export class NewMarkerDialogComponent {

    public readonly marker: Marker;

    public constructor(private dialogRef: MatDialogRef<NewMarkerDialogComponent>) {
        this.marker = new Marker();
    }

    public saveMarker(): void {
        this.dialogRef.close(this.marker);
    }
}
