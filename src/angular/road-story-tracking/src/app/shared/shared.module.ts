import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AngularMaterialModule } from './modules/material.module';

import { DialogService } from './services/dialog/dialog.service';
import { UserService } from './services/user/user.service';
import { UserApiService } from './services/user/user-api.service';
import { ApllicationInterceptor } from './services/http-services/application.interceptor';

import { AuthRequiredComponent } from './components/auth-required/auth-required.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AlertDialogComponent } from './components/dialogs/alert/alert-dialog.component';
import { ConfirmDialogComponent } from './components/dialogs/confirm/confirm-dialog.component';
import { InputTextDialogComponent } from './components/dialogs/input-text/input-text-dialog.component';

const sharedRouting: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'auth-required',
        component: AuthRequiredComponent
    },
    {
        path: 'not-found',
        component: NotFoundComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }
]);

@NgModule({
    imports: [
        AngularMaterialModule,
        sharedRouting
    ],
    providers: [
        UserService,
        UserApiService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ApllicationInterceptor,
            multi: true
        },
        DialogService
    ],
    entryComponents: [
        AlertDialogComponent,
        ConfirmDialogComponent,
        InputTextDialogComponent
    ],
    declarations: [
        AuthRequiredComponent,
        NotFoundComponent,
        AlertDialogComponent,
        ConfirmDialogComponent,
        InputTextDialogComponent
    ]
})
export class SharedModule { }
