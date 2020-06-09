import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { UserService } from './user.service';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + '/auth';
  jwtHelper = new JwtHelperService();
  currentUser: User | null = null;
  photoUrl = new BehaviorSubject<string>('assets/images/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();
  decodedAccessToken: any = {};
  constructor(private http: HttpClient, private userService: UserService) {}

  login(model: any) {
    return this.http.post(this.baseUrl + '/login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('jwt', user.accessToken);
          this.decodedAccessToken = this.jwtHelper.decodeToken(
            user.accessToken
          );
          this.userService.getSelf().subscribe((self: User) => {
            this.currentUser = self;
            this.changeProfilePhotoUrl(self.photoUrl);
          });
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

  changeProfilePhotoUrl = (photoUrl: string) => {
    this.photoUrl.next(photoUrl);
  };
}
