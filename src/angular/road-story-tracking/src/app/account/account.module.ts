import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ManageAccountComponent } from './components/manage-account.component';
import { AuthGuard } from './services/auth-guard.service';

const manageAccountRouting: ModuleWithProviders = RouterModule.forChild([
  {
    path: 'manage-account',
    component: ManageAccountComponent,
    canActivate: [AuthGuard]
  }
]);

@NgModule({
  imports: [manageAccountRouting],
  declarations: [ManageAccountComponent],
  providers: [AuthGuard]
})
export class ManageAccountModule { }
