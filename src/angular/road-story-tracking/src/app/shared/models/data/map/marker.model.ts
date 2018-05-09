import { MarkerOwner } from './marker-owner.model';
import { MarkerType } from './marker-type.enum.model';
import { BaseMarker } from './base-marker.model';
import { MarkerInvitation } from './marker-invitation.model';

export class Marker extends BaseMarker {
    public id: string;
    public createDate: Date;
    public latitude: number;
    public longitude: number;
    public name: string;
    public type: MarkerType;
    public description: string;
    public images: string[];
    public markerOwner: MarkerOwner;
    public invitations: MarkerInvitation[];
    public isPrivate: boolean;
    public startDate: Date;
    public endDate: Date;

    public constructor() {
        super();
        this.type = MarkerType.Other;
        this.images = [];
        this.markerOwner = new MarkerOwner();
        this.invitations = [];
        this.isPrivate = false;
        this.startDate = new Date();
        this.endDate = new Date();
    }
}
