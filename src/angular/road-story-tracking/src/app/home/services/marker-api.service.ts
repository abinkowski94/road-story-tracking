import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { environment } from './../../../environments/environment';

import { BaseHttpService } from '../../shared/services/http-services/base-http.service';
import { Marker } from './../../shared/models/data/map/marker.model';

@Injectable()
export class MarkerApiService extends BaseHttpService {

    public constructor(client: HttpClient) {
        super(client, 'Marker');
    }

    public addMarker(marker: Marker): Observable<Marker> {
        let result = this.post<Marker>('AddMarker', marker);

        if (environment.production === false) {
            result = result.do((resultMarker: Marker) => {
                resultMarker.images = resultMarker.images.map(i => environment.backendHotst + i);
            });
        }

        return result;
    }

    public getMarkers(): Observable<Marker[]> {
        return this.get<Marker[]>('GetMarkers');
    }

    public getMarker(markerId: string): Observable<Marker> {
        const params = new HttpParams().set('markerId', markerId);
        let result = this.get<Marker>('GetMarker', params);

        if (environment.production === false) {
            result = result.do((resultMarker: Marker) => {
                resultMarker.images = resultMarker.images.map(i => environment.backendHotst + i);
            });
        }

        return result;
    }

    public getMyMarkers(): Observable<Marker[]> {
        let result = this.get<Marker[]>('GetMyMarkers');

        if (environment.production === false) {
            result = result.do((resultMarkers: Marker[]) => {
                for (const marker of resultMarkers) {
                    marker.images = marker.images.map(i => environment.backendHotst + i);
                }
            });
        }

        return result;
    }
}
