import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from './../shared/modules/material.module';

import { LoginAccountComponent } from './components/login-account.component';
import { ManageAccountComponent } from './components/manage-account.component';
import { RegisterAccountComponent } from './components/register-account.component';
import { RegisterCompleteComponent } from './components/register-complete.component';
import { ConfirmedAccountComponent } from './components/confirmed-account.component';
import { UserService } from './../shared/services/user.service';
import { AuthGuard } from './../shared/services/auth-guard.service';

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
        ConfirmedAccountComponent
    ],
    providers: [
        AuthGuard
    ]
})
export class ManageAccountModule { }
