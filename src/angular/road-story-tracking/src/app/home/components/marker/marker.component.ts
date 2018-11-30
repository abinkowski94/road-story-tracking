import { NgxGalleryComponent, NgxGalleryImage, NgxGalleryOptions } from 'ngx-gallery';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ParamMap } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { MarkerApiService } from './../../services/marker-api.service';
import { Marker } from './../../../shared/models/data/map/marker.model';
import { MarkerOwner } from './../../../shared/models/data/map/marker-owner.model';
import { BackendErrorResponse } from './../../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../../shared/configurations/snackbar.config';

@Component({
    templateUrl: './marker.component.html',
    styleUrls: ['./marker.component.css']
})
export class MarkerComponent implements OnInit {

    public marker: Marker;
    public galleryImages: NgxGalleryImage[];

    public constructor(private markerApiService: MarkerApiService, private activatedRoute: ActivatedRoute,
        private snackBar: MatSnackBar) { }

    public ngOnInit(): void {
        this.marker = this.activatedRoute.snapshot.data['marker'];
        this.galleryImages = this.marker.images.map((img: string) => ({ big: img, small: img, medium: img }));
    }
}
