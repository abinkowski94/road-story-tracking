import { MatSnackBar } from '@angular/material';
import { Component } from '@angular/core';

import { MarkerService } from './../../services/marker.service';

@Component({
    templateUrl: './filter.component.html',
    styleUrls: ['./filter.component.css'],
    selector: 'app-filter'
})
export class FilterComponent {

    public constructor(private markerService: MarkerService, private snackBar: MatSnackBar) { }
}
