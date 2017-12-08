import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { AngularMaterialModule } from './../shared/modules/material.module';
import { environment } from './../../environments/environment.keys';

import { MarkerService } from './services/marker.service';

import { HomeComponent } from './components/home/home.component';
import { NavigationMenuComponent } from './components/navigation-menu/navigation-menu.component';
import { FilterComponent } from './components/filter/filter.component';

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
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsAPIKey
        })
    ],
    declarations: [
        HomeComponent,
        NavigationMenuComponent,
        FilterComponent
    ],
    providers: [
        MarkerService
    ]
})
export class HomeModule { }
