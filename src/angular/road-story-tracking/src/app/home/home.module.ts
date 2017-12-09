import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { AngularMaterialModule } from './../shared/modules/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { environment } from './../../environments/environment.keys';
import { NgxGalleryModule } from 'ngx-gallery';

import { MarkerService } from './services/marker.service';

import { HomeComponent } from './components/home/home.component';
import { NavigationMenuComponent } from './components/navigation-menu/navigation-menu.component';
import { FilterComponent } from './components/filter/filter.component';
import { NewMarkerDialogComponent } from './components/new-marker/new-marker-dialog.component';

const homeRouting: ModuleWithProviders = RouterModule.forChild([
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'home',
        component: HomeComponent
    }
]);

@NgModule({
    imports: [
        CommonModule,
        AngularMaterialModule,
        homeRouting,
        FormsModule,
        ReactiveFormsModule,
        NgxGalleryModule,
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsAPIKey
        })
    ],
    declarations: [
        HomeComponent,
        NavigationMenuComponent,
        FilterComponent,
        NewMarkerDialogComponent
    ],
    entryComponents: [
        NewMarkerDialogComponent
    ],
    providers: [
        MarkerService
    ]
})
export class HomeModule { }
