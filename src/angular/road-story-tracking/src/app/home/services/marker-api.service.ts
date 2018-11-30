import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from './../../../environments/environment';

import { BaseHttpService } from '../../shared/services/http-services/base-http.service';
import { Marker } from './../../shared/models/data/map/marker.model';
import { IncomingMarkerInviation } from '../../shared/models/data/map/incoming-marker-invitation.model';
import { InvitationStatuses } from '../../shared/models/data/map/invitation-statuses.enum.model';

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

    public getIncomingInvitations(): Observable<IncomingMarkerInviation[]> {
        return this.get<IncomingMarkerInviation[]>('GetMyIncomingMarkersInvitations');
    }
    public updateMarkerInvitation(invitationId: string, value: InvitationStatuses): Observable<IncomingMarkerInviation> {
        const params = new HttpParams().set('invitationId', invitationId).set('invitationStatus', `${value}`);
        return this.put<IncomingMarkerInviation>('UpdateMarkerInvitationStatus', null, params);
    }

    public deleteInvitation(invitationId: string): Observable<IncomingMarkerInviation> {
        const params = new HttpParams().set('invitationId', invitationId);
        return this.delete<IncomingMarkerInviation>('DeleteMarkerInvitation', params);
    }
}
