import { NgModule } from '@angular/core';
import {
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatGridListModule,
    MatDialogModule,
    MatSelectModule,
    MatTableModule,
    MatMenuModule
} from '@angular/material';

@NgModule({
    imports: [
        MatButtonModule,
        MatCheckboxModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatSnackBarModule,
        MatFormFieldModule,
        MatGridListModule,
        MatDialogModule,
        MatSelectModule,
        MatTableModule,
        MatMenuModule
    ],
    exports: [
        MatButtonModule,
        MatCheckboxModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatSnackBarModule,
        MatFormFieldModule,
        MatGridListModule,
        MatDialogModule,
        MatSelectModule,
        MatTableModule,
        MatMenuModule
    ]
})
export class AngularMaterialModule { }
