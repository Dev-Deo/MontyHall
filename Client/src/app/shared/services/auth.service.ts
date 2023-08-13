import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ICurrentUser } from '../models/currentUser';
import { IApiResponse } from '../models/apiResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  private readonly JWT_TOKEN = 'token';

  private currentUserSource = new ReplaySubject<ICurrentUser>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    this.currentUserSource.next(this.getCurrentUser());
  }

  login(values: any) {
    return this.http
      .post<IApiResponse<ICurrentUser>>(this.baseUrl + 'account/login', values)
      .pipe(
        map((user) => {
          if (user) {
            localStorage.setItem('currentUser', JSON.stringify(user.data));
            this.storeTokens(user.data.token);
            this.currentUserSource.next(user.data);
          }
        })
      );
  }

  logout() {
    this.removeTokens();
    localStorage.removeItem('currentUser');
    this.currentUserSource.next(null);
    this.router.navigate(['/auth/login']);
  }

  getCurrentUser() {
    let user: ICurrentUser = JSON.parse(localStorage.getItem('currentUser'));
    return user;
  }

  // get stored access token
  public getJwtToken() {
    return localStorage.getItem(this.JWT_TOKEN);
  }

  // store access token and refresh token
  private storeTokens(tokens: string) {
    localStorage.setItem(this.JWT_TOKEN, tokens);
  }

  // remove stored tokens
  private removeTokens() {
    localStorage.removeItem(this.JWT_TOKEN);
  }
}
