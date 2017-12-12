import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
    templateUrl: 'input-text-dialog.component.html'
})
export class InputTextDialogComponent {

    public title: string;
    public placeholder: string;

    public constructor(public dialogRef: MatDialogRef<InputTextDialogComponent>) { }

    public closeWithResponse(value: string): void {
        this.dialogRef.close(value);
    }
}
