import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ItemSelectComponent } from './components/item-select/item-select.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DataTableComponent } from './components/data-table/data-table.component';

@NgModule({
  declarations: [TextInputComponent, ItemSelectComponent, DataTableComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgxDatatableModule,
    NgSelectModule,
    SweetAlert2Module.forRoot(),
    BsDatepickerModule.forRoot(),
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    NgxDatatableModule,
    NgSelectModule,
    TextInputComponent,
    SweetAlert2Module,
    ItemSelectComponent,
    BsDatepickerModule,
    DataTableComponent,
  ],
})
export class SharedModule {}
