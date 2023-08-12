import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserManagerService } from 'src/app/shared/services/user-manager.service';

@Component({
  selector: 'app-user-manager-edit',
  templateUrl: './user-manager-edit.component.html',
  styleUrls: ['./user-manager-edit.component.scss'],
})
export class UserManagerEditComponent implements OnInit {
  applicationUserForm: FormGroup;
  applicationUserName: string;

  constructor(
    private fb: FormBuilder,
    private userManagerService: UserManagerService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const applicationUserId = params.get('id');
      this.createApplicationUserForm();
      this.getApplicationUserById(applicationUserId);
    });
  }

  getApplicationUserById(applicationUserId: string) {
    this.userManagerService.getUserById(applicationUserId).subscribe({
      next: (responce) => {
        this.applicationUserForm.patchValue({
          id: responce.data.id,
          firstName: responce.data.firstName,
          lastName: responce.data.lastName,
          contactNo: responce.data.contactNo,
        });

        this.applicationUserName =
          responce.data.firstName + ' ' + responce.data.lastName;
        console.log(this.applicationUserForm.value);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  createApplicationUserForm() {
    this.applicationUserForm = this.fb.group({
      id: [null, [Validators.required]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      contactNo: [null, [Validators.required]],
    });
  }

  onSubmit() {
    this.userManagerService
      .updateUser(this.applicationUserForm.value)
      .subscribe({
        next: (responce) => {
          this.router.navigate(['/', 'user-manager']);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
