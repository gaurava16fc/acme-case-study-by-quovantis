import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';

// Http Options
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiBaseUrl = '';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {
    this.apiBaseUrl = environment.apiBaseUrl + 'auth/';
   }

  login(model: any) {
    return this.http.post(this.apiBaseUrl + 'login', JSON.stringify(model), httpOptions).pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', user.user);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
          }
        })
      );
  }

  // register(model: any) {
  //   return this.http.post(this.apiBaseUrl + 'register', model);
  // }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  isEmployeeRole(): boolean {
    const userFromLocalStorage = localStorage.getItem('user');
    if (userFromLocalStorage != null) {
      let _userDetails = JSON.parse(userFromLocalStorage);
      if (_userDetails) {
        return (_userDetails.UserRole.RoleName === 'Employee') ? true : false;
      }
    }
    return false;
  }

  isCustomerRole(): boolean {
    const userFromLocalStorage = localStorage.getItem('user');
    if (userFromLocalStorage != null) {
      let _userDetails = JSON.parse(userFromLocalStorage);
      if (_userDetails) {
        return (_userDetails.UserRole.RoleName === 'Customer') ? true : false;
      }
    }
    return false;
  }
}
