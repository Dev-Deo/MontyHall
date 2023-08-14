import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserManagerService } from 'src/app/shared/services/user-manager.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  applicationUserForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userManagerService: UserManagerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.createApplicationUserForm();
  }

  createApplicationUserForm() {
    this.applicationUserForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      contactNo: [null, [Validators.required]],
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  onSubmit() {
    this.userManagerService
      .createUser(this.applicationUserForm.value)
      .subscribe({
        next: (responce) => {
          this.router.navigate(['/', 'auth']);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
