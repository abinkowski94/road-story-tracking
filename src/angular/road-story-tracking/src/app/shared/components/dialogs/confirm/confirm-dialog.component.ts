import { MatDialogRef } from '@angular/material';
import { Component } from '@angular/core';

@Component({
    templateUrl: 'confirm-dialog.component.html'
})
export class ConfirmDialogComponent {

    public title: string;
    public message: string;

    public constructor(public dialogRef: MatDialogRef<ConfirmDialogComponent>) { }
}
