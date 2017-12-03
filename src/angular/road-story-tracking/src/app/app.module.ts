import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './main/app.component';
import { NavigationBarComponent } from './shared/components/navigation-bar/navigation-bar.component';
import { AngularMaterialModule } from './shared/modules/material.module';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { ManageAccountModule } from './account/account.module';

@NgModule({
    declarations: [
        AppComponent,
        NavigationBarComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AngularMaterialModule,
        HttpClientModule,
        HomeModule,
        ManageAccountModule,
        SharedModule,
        RouterModule.forRoot([], { useHash: false })
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
