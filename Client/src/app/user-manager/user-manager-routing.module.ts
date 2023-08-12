import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserManagerComponent } from './user-manager.component';
import { UserManagerCreateComponent } from './user-manager-create/user-manager-create.component';
import { UserManagerEditComponent } from './user-manager-edit/user-manager-edit.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'details',
    pathMatch: 'full',
  },
  {
    path: 'details',
    component: UserManagerComponent,
  },
  {
    path: 'create',
    component: UserManagerCreateComponent,
  },
  {
    path: 'edit/:id',
    component: UserManagerEditComponent,
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagerRoutingModule {}
