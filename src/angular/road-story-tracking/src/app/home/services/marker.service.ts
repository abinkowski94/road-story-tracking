import { Injectable } from '@angular/core';
import { Observable,  BehaviorSubject } from 'rxjs';

import { MarkerApiService } from './marker-api.service';
import { MarkerServiceState } from './marker-service-state.enum';
import { Marker } from './../../shared/models/data/map/marker.model';
import { MarkerType } from '../../shared/models/data/map/marker-type.enum.model';

@Injectable()
export class MarkerService {

    private _state: BehaviorSubject<MarkerServiceState>;
    private _staticMarkers: Marker[];
    private _markers: BehaviorSubject<Marker[]>;

    public readonly state: Observable<MarkerServiceState>;
    public readonly markers: Observable<Marker[]>;

    public constructor(private markerApiService: MarkerApiService) {
        this._staticMarkers = [];
        this._state = new BehaviorSubject(MarkerServiceState.None);
        this._markers = new BehaviorSubject<Marker[]>(this._staticMarkers);
        this.markers = this._markers.asObservable();
        this.state = this._state.asObservable();

        this.getMarkers();
    }

    public setState(state: MarkerServiceState): void {
        this._state.next(state);
    }

    public getMarkers(): void {
        this.markerApiService.getMarkers().subscribe((result: Marker[]) => {
            this._staticMarkers = result;
            this._markers.next(this._staticMarkers);
        });
    }

    public addMarker(marker: Marker): Observable<Marker> {
        return this.markerApiService.addMarker(marker).do((result: Marker) => {
            this._staticMarkers.push(result);
            this._markers.next(this._staticMarkers);
            this.setState(MarkerServiceState.None);
        });
    }

    public translateTypeToIcon(type: MarkerType): string {
        if (type === MarkerType.NeedARide) {
            return 'assets/icons/car-marker.png';
        } else if (type === MarkerType.CashRelated) {
            return 'assets/icons/cash-marker.png';
        } else if (type === MarkerType.Party) {
            return 'assets/icons/party-marker.png';
        }
        return '';
    }
}
