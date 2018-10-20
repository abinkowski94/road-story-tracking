import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatDialog, MatDialogConfig } from '@angular/material';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryComponent } from 'ngx-gallery';

import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerInvitation } from './../../../shared/models/data/map/marker-invitation.model';
import { MarkerApiService } from './../../services/marker-api.service';
import { ImageService } from './../../../shared/services/image-services/image.service';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';
import { MarkerInvitationMenuComponent } from '../marker-invitation-menu/marker-invitation-menu.component';

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
        private markerApiService: MarkerApiService, @Inject(MAT_DIALOG_DATA) private data: Marker, private snackBar: MatSnackBar,
        private materialdialogService: MatDialog) {

        this.marker = this.data;
        this.galleryImages = this.data.images.map(i => ({ small: i, medium: i, big: i }));
        this.galleryOptions = [
            { thumbnails: false, height: '200px', width: '100%' },
            { breakpoint: 500, height: '200px' },
            { breakpoint: 400, height: '100px' }
        ];
    }

    public async updateMarker(): Promise<void> {
        if (new Date(this.marker.startDate) > new Date(this.marker.endDate)) {
            this.snackBar.open('The start date is lower than end date.', 'Error!', snackbarConfiguration);
        } else {
            try {
                const result = await this.markerApiService.updateMarker(this.marker).toPromise();
                this.dialogRef.close(result);
                this.snackBar.open('Event has been updated.', 'Success!', snackbarConfiguration);
            } catch (exception) {
                this.snackBar.open('Cannot update event at the server side.', 'Error!', snackbarConfiguration);
            }
        }
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

    public async manageInvitations(): Promise<void> {
        const options: MatDialogConfig<MarkerInvitation[]> = {
            data: this.marker.invitations,
            width: '80%'
        };
        const result = await this.materialdialogService.open(MarkerInvitationMenuComponent, options).afterClosed().toPromise();
        if (result) {
            this.marker.invitations = result;
        }
    }
}
