import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { UserManagerRoutingModule } from './user-manager-routing.module';
import { UserManagerComponent } from './user-manager.component';
import { UserManagerCreateComponent } from './user-manager-create/user-manager-create.component';
import { UserManagerEditComponent } from './user-manager-edit/user-manager-edit.component';
import { CoreModule } from "../core/core.module";

@NgModule({
    declarations: [UserManagerComponent, UserManagerCreateComponent, UserManagerEditComponent],
    imports: [CommonModule, UserManagerRoutingModule, SharedModule, CoreModule]
})
export class UserManagerModule {}
