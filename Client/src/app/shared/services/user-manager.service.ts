import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IApiResponse } from '../models/apiResponse';
import { IApplicationUser } from '../models/applicationUser';
import { IApplicationUserCreate } from '../models/applicationUserCreate';
import { IApplicationUserUpdate } from '../models/applicationUserUpdate';

@Injectable({
  providedIn: 'root',
})
export class UserManagerService implements OnInit {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  getUsers() {
    return this.http.get<IApiResponse<IApplicationUser[]>>(
      this.baseUrl + 'userManger'
    );
  }

  getUserById(id: string) {
    return this.http.get<IApiResponse<IApplicationUser>>(
      this.baseUrl + 'userManger/' + id
    );
  }

  createUser(user: IApplicationUserCreate) {
    return this.http.post<IApiResponse<IApplicationUser>>(
      this.baseUrl + 'userManger',
      user
    );
  }

  updateUser(user: IApplicationUserUpdate) {
    return this.http.put<IApiResponse<IApplicationUser>>(
      this.baseUrl + 'userManger',
      user
    );
  }
}
