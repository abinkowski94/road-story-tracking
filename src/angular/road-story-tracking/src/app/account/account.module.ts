import { LoginAccountComponent } from './components/login-account.component';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { ManageAccountComponent } from './components/manage-account.component';
import { RegisterAccountComponent } from './components/register-account.component';
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
    }
]);

@NgModule({
    imports: [FormsModule, manageAccountRouting],
    declarations: [
        ManageAccountComponent,
        LoginAccountComponent,
        RegisterAccountComponent
    ],
    providers: [
        AuthGuard
    ]
})
export class ManageAccountModule { }
