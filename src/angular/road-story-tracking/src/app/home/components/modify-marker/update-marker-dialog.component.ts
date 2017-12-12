import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryComponent } from 'ngx-gallery';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerApiService } from './../../services/marker-api.service';
import { ImageService } from './../../../shared/services/image-services/image.service';

@Component({
    templateUrl: 'update-marker-dialog.component.html',
    styleUrls: ['update-marker-dialog.component.css']
})
export class UpdateMarkerDialogComponent {

    @ViewChild('gallery') public gallery: NgxGalleryComponent;
    public readonly marker: Marker;
    public galleryOptions: NgxGalleryOptions[];
    public galleryImages: NgxGalleryImage[];

    public constructor(private imageService: ImageService, private dialogRef: MatDialogRef<UpdateMarkerDialogComponent>,
        private markerApiService: MarkerApiService, @Inject(MAT_DIALOG_DATA) private data: Marker, private snackBar: MatSnackBar) {

        this.marker = this.data;
        this.galleryImages = this.data.images.map(i => ({ small: i, medium: i, big: i }));
        this.galleryOptions = [
            { thumbnails: false, height: '200px', width: '100%' },
            { breakpoint: 500, height: '200px' },
            { breakpoint: 400, height: '100px' }
        ];
    }

    public updateMarker(): void {
        this.markerApiService.updateMarker(this.marker).subscribe((result: Marker) => {
            this.dialogRef.close(result);
        }, error => {
            this.snackBar.open('Cannot update event at the server side.', 'Error!', {
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
