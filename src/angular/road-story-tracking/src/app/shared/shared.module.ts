import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AngularMaterialModule } from './modules/material.module';

import { AuthRequiredComponent } from './components/auth-required.component';
import { NotFoundComponent } from './components/not-found.component';
import { UserService } from './services/user.service';
import { UserApiService } from './services/user-api.service';
import { ApllicationInterceptor } from './services/application.interceptor';


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
        }
    ],
    declarations: [
        AuthRequiredComponent,
        NotFoundComponent
    ]
})
export class SharedModule { }
