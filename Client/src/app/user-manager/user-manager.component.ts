import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { IApplicationUser } from '../shared/models/applicationUser';
import { UserManagerService } from '../shared/services/user-manager.service';

@Component({
  selector: 'app-user-manager',
  templateUrl: './user-manager.component.html',
  styleUrls: ['./user-manager.component.scss'],
})
export class UserManagerComponent implements OnInit {
  @ViewChild('actionTemplate', { static: true })
  actionTemplate!: TemplateRef<any>;

  applicationUserSource = new BehaviorSubject<IApplicationUser[]>(null);

  columns = [];
  constructor(
    private userManagerService: UserManagerService,
    private router: Router
  ) {
    this.getUsers();
  }

  ngOnInit(): void {
    this.columns = [
      { name: 'Email', prop: 'email', sortable: true },
      { name: 'First Name', prop: 'firstName', sortable: true },
      { name: 'Last Name', prop: 'lastName', sortable: true },
      { name: 'Contact No', prop: 'contactNo', sortable: true },
      { name: 'Total Attempt', prop: 'Total Attempt', sortable: true },
      {
        name: 'Action',
        prop: 'id',
        sortable: false,
        cellTemplate: this.actionTemplate,
      },
    ];
  }

  getUsers() {
    this.userManagerService.getUsers().subscribe({
      next: (res) => {
        this.applicationUserSource.next(res.data);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {},
    });
  }

  onEdit(id: string) {
    this.router.navigate(['/user-manager/edit', id]);
  }
}
