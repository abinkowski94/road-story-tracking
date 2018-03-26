import { NgModule, ModuleWithProviders } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { NgxGalleryModule } from 'ngx-gallery';

import { AngularMaterialModule } from './../shared/modules/material.module';
import { environment } from './../../environments/environment.keys';

import { MarkerApiService } from './services/marker-api.service';
import { MarkerService } from './services/marker.service';
import { CommentApiService } from './services/comment-api.service';
import { ImageService } from './../shared/services/image-services/image.service';
import { AuthGuard } from './../shared/services/user/auth-guard.service';
import { DialogService } from './../shared/services/dialog/dialog.service';

import { MarkerResolver } from './resolvers/marker-resolver.service';

import { HomeComponent } from './components/home/home.component';
import { FilterComponent } from './components/filter/filter.component';
import { MarkerComponent } from './components/marker/marker.component';
import { CommentsComponent } from './components/comment/comments.component';
import { NavigationMenuComponent } from './components/navigation-menu/navigation-menu.component';
import { NewMarkerDialogComponent } from './components/new-marker/new-marker-dialog.component';
import { MyMarkersComponent } from './components/my-markers/my-markers.component';
import { UpdateMarkerDialogComponent } from './components/modify-marker/update-marker-dialog.component';

const homeRouting: ModuleWithProviders = RouterModule.forChild([
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'marker/:id',
        component: MarkerComponent,
        resolve: { marker: MarkerResolver }
    },
    {
        path: 'my-markers',
        component: MyMarkersComponent,
        canActivate: [AuthGuard]
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
        NewMarkerDialogComponent,
        MarkerComponent,
        MyMarkersComponent,
        UpdateMarkerDialogComponent,
        CommentsComponent
    ],
    entryComponents: [
        NewMarkerDialogComponent,
        UpdateMarkerDialogComponent
    ],
    providers: [
        MarkerApiService,
        MarkerService,
        ImageService,
        AuthGuard,
        DialogService,
        CommentApiService,
        MarkerResolver
    ]
})
export class HomeModule { }
