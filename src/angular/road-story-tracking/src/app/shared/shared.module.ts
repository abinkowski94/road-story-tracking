import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthRequiredComponent } from './components/auth-required.component';
import { NotFoundComponent } from './components/not-found.component';

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
  imports: [sharedRouting],
  declarations: [AuthRequiredComponent, NotFoundComponent]
})
export class SharedModule { }
