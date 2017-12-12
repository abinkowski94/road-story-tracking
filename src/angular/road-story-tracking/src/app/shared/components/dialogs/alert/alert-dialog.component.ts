import { MatDialogRef } from '@angular/material';
import { Component } from '@angular/core';

@Component({
    templateUrl: 'alert-dialog.component.html'
})
export class AlertDialogComponent {

    public title: string;
    public message: string;

    public constructor(public dialogRef: MatDialogRef<AlertDialogComponent>) { }
}
