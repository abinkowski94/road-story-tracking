import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryComponent } from 'ngx-gallery';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerService } from './../../services/marker.service';
import { ImageService } from './../../../shared/services/image-services/image.service';

@Component({
    templateUrl: 'new-marker-dialog.component.html',
    styleUrls: ['new-marker-dialog.component.css'],
})
export class NewMarkerDialogComponent {

    @ViewChild('gallery') public gallery: NgxGalleryComponent;
    public readonly marker: Marker;
    public galleryOptions: NgxGalleryOptions[];
    public galleryImages: NgxGalleryImage[];

    public constructor(private imageService: ImageService, private dialogRef: MatDialogRef<NewMarkerDialogComponent>,
        private markerService: MarkerService, @Inject(MAT_DIALOG_DATA) private data: any, private snackBar: MatSnackBar) {

        this.marker = new Marker();
        this.marker.latitude = this.data.latitude;
        this.marker.longitude = this.data.longitude;
        this.galleryImages = [];
        this.galleryOptions = [
            { thumbnails: false, height: '200px', width: '100%' },
            { breakpoint: 500, height: '200px' },
            { breakpoint: 400, height: '100px' }
        ];
    }

    public saveMarker(): void {
        this.markerService.addMarker(this.marker).subscribe((result: Marker) => {
            this.dialogRef.close(result);
        }, error => {
            this.snackBar.open('Cannot save event at the server side.', 'Error!', {
                duration: 3000,
                horizontalPosition: 'right'
            });
        });
    }

    public addImage(input: any): void {
        this.imageService.loadImage(input, (loadedImage: string) => {
            this.galleryImages.push({
                small: loadedImage,
                medium: loadedImage,
                big: loadedImage
            });
            this.marker.images.push(loadedImage);
        });
    }

    public removeImage(): void {
        let lastShown = this.gallery.selectedIndex;
        this.galleryImages.splice(lastShown, 1);
        this.marker.images.splice(lastShown, 1);
        if (lastShown > 0) {
            lastShown--;
        }
        this.gallery.show(lastShown);
    }
}
