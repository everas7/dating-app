import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth';
  jwtHelper = new JwtHelperService();
  decodedAccessToken: any = {};
  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + '/login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('jwt', user.accessToken);
          this.decodedAccessToken = this.jwtHelper.decodeToken(user.accessToken);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + '/register', model);
  }

  loggedIn() {
    const accessToken = localStorage.getItem('jwt');
    return !this.jwtHelper.isTokenExpired(accessToken);
  }
}