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
    MatAutocompleteModule,
    MatTabsModule,
    MatExpansionModule,
    MatListModule,
    MatProgressBarModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MAT_DATE_LOCALE,
    MatChipsModule
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
        MatAutocompleteModule,
        MatTabsModule,
        MatExpansionModule,
        MatListModule,
        MatProgressBarModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatChipsModule
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
        MatAutocompleteModule,
        MatTabsModule,
        MatExpansionModule,
        MatListModule,
        MatProgressBarModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatChipsModule
    ],
    providers: [
        {provide: MAT_DATE_LOCALE, useValue: 'en-GB'}
    ]
})
export class AngularMaterialModule { }
