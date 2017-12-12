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
    MatMenuModule,
    MatAutocompleteModule
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
        MatMenuModule,
        MatAutocompleteModule
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
        MatMenuModule,
        MatAutocompleteModule
    ]
})
export class AngularMaterialModule { }
