import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from './../shared/modules/material.module';

import { LoginAccountComponent } from './components/login-account/login-account.component';
import { ManageAccountComponent } from './components/manage-account/manage-account.component';
import { RegisterAccountComponent } from './components/register-account/register-account.component';
import { RegisterCompleteComponent } from './components/register-complete/register-complete.component';
import { ConfirmedAccountComponent } from './components/confirmed-account/confirmed-account.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

import { UserService } from './../shared/services/user/user.service';
import { ManageAccountApiService } from './services/manage-account-api.service';
import { AuthGuard } from './../shared/services/user/auth-guard.service';

const manageAccountRouting: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'account/manage',
        component: ManageAccountComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'account/login',
        component: LoginAccountComponent
    },
    {
        path: 'account/register',
        component: RegisterAccountComponent
    },
    {
        path: 'account/register/complete',
        component: RegisterCompleteComponent
    },
    {
        path: 'account/register/confirmed',
        component: ConfirmedAccountComponent
    },
    {
        path: 'account/reset',
        component: ResetPasswordComponent
    }
]);

@NgModule({
    imports: [
        AngularMaterialModule,
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        manageAccountRouting
    ],
    declarations: [
        ManageAccountComponent,
        LoginAccountComponent,
        RegisterAccountComponent,
        RegisterCompleteComponent,
        ConfirmedAccountComponent,
        ResetPasswordComponent
    ],
    providers: [
        AuthGuard,
        ManageAccountApiService
    ]
})
export class ManageAccountModule { }
