import { NgxGalleryComponent, NgxGalleryImage, NgxGalleryOptions } from 'ngx-gallery';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ParamMap } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { MarkerApiService } from './../../services/marker-api.service';
import { Marker } from './../../../shared/models/data/map/marker.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';

@Component({
    templateUrl: './marker.component.html',
    styleUrls: ['./marker.component.css']
})
export class MarkerComponent implements OnInit, OnDestroy {

    private subscription: Subscription;

    @ViewChild('gallery') public gallery: NgxGalleryComponent;
    public marker: Marker;
    public galleryOptions: NgxGalleryOptions[];
    public galleryImages: NgxGalleryImage[];

    public constructor(private markerApiService: MarkerApiService, private activatedRoute: ActivatedRoute, private snackBar: MatSnackBar) {
        this.marker = new Marker();
        this.galleryOptions = [{ thumbnails: false, height: '400px', width: '100%' }];
        this.galleryImages = [];
    }

    public ngOnInit(): void {
        this.subscription = this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
            this.markerApiService.getMarker(params.get('id')).subscribe((result: Marker) => {
                this.marker = result;
                this.galleryImages = result.images.map((img: string) => ({ big: img, small: img, medium: img }));
            }, (error: HttpErrorResponse) => {
                const errorMessage = (error.error as BackendErrorResponse).exception.Message;
                this.marker.name = errorMessage;
                this.snackBar.open(errorMessage, 'Error!', {
                    duration: 3000,
                    horizontalPosition: 'right'
                });
            });
        });
    }

    public ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
}
