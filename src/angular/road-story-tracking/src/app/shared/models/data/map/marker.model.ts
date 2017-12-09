import { MarkerType } from './marker-type.enum.model';
import { BaseMarker } from './base-marker.model';

export class Marker extends BaseMarker {
    public latitude: number;
    public longitude: number;
    public name: string;
    public type: MarkerType;
    public description: string;
    public imageUrls: string[];

    public constructor() {
        super();
        this.type = MarkerType.Other;
        this.imageUrls = [];
    }
}
