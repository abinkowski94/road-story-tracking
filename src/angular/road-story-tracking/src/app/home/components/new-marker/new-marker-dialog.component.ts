import { Component, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryComponent } from 'ngx-gallery';

@Component({
    templateUrl: 'new-marker-dialog.component.html',
    styleUrls: ['new-marker-dialog.component.css'],
})
export class NewMarkerDialogComponent {

    @ViewChild('gallery') public gallery: NgxGalleryComponent;
    public readonly marker: Marker;
    public galleryOptions: NgxGalleryOptions[];
    public galleryImages: NgxGalleryImage[];

    public constructor(private dialogRef: MatDialogRef<NewMarkerDialogComponent>) {
        this.marker = new Marker();
        this.galleryImages = [];
        this.galleryOptions = [
            { thumbnails: false, height: '200px', width: '100%' },
            { breakpoint: 500, height: '200px' },
            { breakpoint: 400, height: '100px' }
        ];
    }

    public saveMarker(): void {
        this.dialogRef.close(this.marker);
    }

    public addImage(): void {

    }

    public removeImage(): void {
        let lastShown = this.gallery.selectedIndex;
        this.galleryImages.splice(lastShown, 1);
        this.marker.imageUrls.splice(lastShown, 1);
        if (lastShown > 0) {
            lastShown--;
        }
        this.gallery.show(lastShown);
    }
}
