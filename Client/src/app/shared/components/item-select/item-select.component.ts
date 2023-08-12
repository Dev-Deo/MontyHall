import { Component, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { NgSelectComponent } from '@ng-select/ng-select';

@Component({
  selector: 'app-item-select',
  templateUrl: './item-select.component.html',
  styleUrls: ['./item-select.component.scss'],
})
export class ItemSelectComponent implements OnInit, ControlValueAccessor {
  @ViewChild('select', { static: true }) select: NgSelectComponent;
  @Input() items: any = [];
  @Input() bindValue: string = 'id';
  @Input() bindLabel: string = 'name';
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() multiple: boolean = false;

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}

  ngOnInit(): void {}

  get control(): FormControl {
    return this.controlDir.control as FormControl;
  }
}
