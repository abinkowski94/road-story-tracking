import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from './../shared/modules/material.module';


import { UserService } from './../shared/services/user/user.service';
import { AuthGuard } from './../shared/services/user/auth-guard.service';
import { FriendsComponent } from './components/friends/friends.component';

const friendsRouting: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'my-friends',
        component: FriendsComponent,
        canActivate: [AuthGuard]
    }
]);

@NgModule({
    imports: [
        AngularMaterialModule,
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        friendsRouting
    ],
    declarations: [FriendsComponent],
    providers: [
        AuthGuard
    ]
})
export class FriendsModule { }
