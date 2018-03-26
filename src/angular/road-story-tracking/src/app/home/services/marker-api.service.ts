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
        return this.post<Marker>('AddMarker', marker);
    }

    public getMarkers(): Observable<Marker[]> {
        return this.get<Marker[]>('GetMarkers');
    }

    public getMarker(markerId: string): Observable<Marker> {
        const params = new HttpParams().set('markerId', markerId);
        return this.get<Marker>('GetMarker', params);
    }

    public getMyMarkers(): Observable<Marker[]> {
        return this.get<Marker[]>('GetMyMarkers');
    }

    public deleteMarker(markerId: string): Observable<Marker> {
        const params = new HttpParams().set('markerId', markerId);
        return this.delete<Marker>('DeleteMarker', params);
    }

    public updateMarker(marker: Marker): Observable<Marker> {
        return this.put<Marker>('UpdateMarker', marker);
    }
}
