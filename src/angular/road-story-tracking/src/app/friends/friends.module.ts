import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from './../shared/modules/material.module';


import { UserService } from './../shared/services/user/user.service';
import { FriendsApiService } from './services/friends-api.service';
import { AuthGuard } from './../shared/services/user/auth-guard.service';
import { FriendsComponent } from './components/friends/friends.component';
import { FriendsListComponent } from './components/friends-list/friends-list.component';
import { FriendsSearchComponent } from './components/friends-search/friends-search.component';
import { IncomingInvitationsComponent } from './components/incoming-invitations/incoming-invitations.component';

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
    declarations: [FriendsComponent, FriendsListComponent, FriendsSearchComponent, IncomingInvitationsComponent],
    providers: [
        AuthGuard,
        FriendsApiService
    ]
})
export class FriendsModule { }
