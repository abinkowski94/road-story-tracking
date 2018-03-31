import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { ManageAccountModule } from './account/account.module';
import { FriendsModule } from './friends/friends.module';
import { AngularMaterialModule } from './shared/modules/material.module';

import { AppComponent } from './main/app.component';
import { NavigationBarComponent } from './shared/components/navigation-bar/navigation-bar.component';

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
        FriendsModule,
        SharedModule,
        RouterModule.forRoot([], { useHash: false })
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
