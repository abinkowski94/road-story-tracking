import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/do';

import { MarkerServiceState } from './marker-service-state.enum';
import { Marker } from './../../shared/models/data/map/marker.model';

@Injectable()
export class MarkerService {

    private _staticMarkers: Marker[];
    private _markers: BehaviorSubject<Marker[]>;
    private _state: BehaviorSubject<MarkerServiceState>;
    public readonly markers: Observable<Marker[]>;
    public readonly state: Observable<MarkerServiceState>;

    public constructor() {
        this._staticMarkers = [];

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position: Position) => {

                const marker = new Marker();
                marker.latitude = position.coords.latitude;
                marker.longitude = position.coords.longitude;
                marker.iconUrl = 'assets/icons/my-position-marker.png';
                marker.name = 'My position';

                this._staticMarkers.push(marker);
            });
        }

        this._state = new BehaviorSubject(MarkerServiceState.None);
        this._markers = new BehaviorSubject<Marker[]>(this._staticMarkers);
        this.markers = this._markers.asObservable();
        this.state = this._state.asObservable();
    }

    public setState(state: MarkerServiceState): void {
        this._state.next(state);
    }

    public addMarker(marker: Marker): void {
        this._staticMarkers.push(marker);
        this._markers.next(this._staticMarkers);
    }
}
