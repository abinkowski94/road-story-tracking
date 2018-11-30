import { MatSnackBar } from '@angular/material';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { MarkerApiService } from '../services/marker-api.service';
import { Marker } from '../../shared/models/data/map/marker.model';
import { BackendErrorResponse } from '../../shared/models/responses/error-response.model';
import { snackbarConfiguration } from '../../shared/configurations/snackbar.config';

@Injectable()
export class MarkerResolver implements Resolve<Marker> {

    constructor(private markerApiService: MarkerApiService, private snackBar: MatSnackBar) { }

    public async resolve(route: ActivatedRouteSnapshot): Promise<Marker> {
        try {
            return await this.markerApiService.getMarker(route.params['id']).toPromise();
        } catch (error) {
            const errorMessage = (error.error as BackendErrorResponse).exception.message;
            this.snackBar.open(errorMessage, 'Error!', snackbarConfiguration);
        }
    }
}
