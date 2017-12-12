import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Observable } from 'rxjs/Observable';

import { AlertDialogComponent } from './../../components/dialogs/alert/alert-dialog.component';
import { ConfirmDialogComponent } from './../../components/dialogs/confirm/confirm-dialog.component';
import { InputTextDialogComponent } from './../../components/dialogs/input-text/input-text-dialog.component';

@Injectable()
export class DialogService {

    public constructor(private dialog: MatDialog) { }

    public confirm(title: string, message: string): Observable<boolean> {

        const dialogRef = this.dialog.open(ConfirmDialogComponent);
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.message = message;

        return dialogRef.afterClosed();
    }

    public alert(title: string, message: string): Observable<boolean> {

        const dialogRef = this.dialog.open(AlertDialogComponent);
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.message = message;

        return dialogRef.afterClosed();
    }

    public inputText(title: string, placeholder: string): Observable<string> {

        const dialogRef = this.dialog.open(InputTextDialogComponent);
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.placeholder = placeholder;

        return dialogRef.afterClosed();
    }
}