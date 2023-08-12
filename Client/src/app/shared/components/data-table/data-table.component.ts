import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'],
})
export class DataTableComponent implements OnInit {
  @Input() title: string = '';
  @Input() columns: [];
  @Input('data') dataSource: BehaviorSubject<any>;

  @Output('filter') tableUpdateFilter = new EventEmitter<string>();

  selectedRowCount: number = 10;

  rowsSrc: BehaviorSubject<any[]> = new BehaviorSubject<any[]>(null);
  rows$: Observable<any[]> = this.rowsSrc.asObservable();

  ColumnMode = ColumnMode;

  search: string = '';

  rowCounts = [5, 10, 15, 20, 25];

  constructor() {}

  ngOnInit(): void {
    this.dataSource.subscribe({
      next: (src) => {
        if (src) {
          this.rowsSrc.next(src);
        }
      },
    });
  }

  updateFilter(event) {
    this.tableUpdateFilter.emit(event);
  }

  setPage(pageInfo) {
    // this.tablePageInfo.emit(pageInfo);
  }

  onRowCountChange() {
    // this.tableSelectedRowCount.emit(this.selectedRowCount);
  }

  onSort(event) {
    // this.tableSort.emit(event);
  }
}
