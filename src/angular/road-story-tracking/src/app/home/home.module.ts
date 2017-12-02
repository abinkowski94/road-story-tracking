import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { AngularMaterialModule } from './../shared/modules/material.module';
import { environment } from './../../environments/environment.keys';

import { HomeComponent } from './components/home.component';

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
        AngularMaterialModule,
        homeRouting,
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsAPIKey
          })
        ],
    declarations: [HomeComponent]
})
export class HomeModule { }
