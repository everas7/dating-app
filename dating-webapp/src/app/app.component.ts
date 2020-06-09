import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserService } from './_services/user.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'dating-webapp';
  jwtHelper = new JwtHelperService();

  constructor(
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit() {
    const accessToken = localStorage.getItem('jwt');
    if (accessToken) {
      this.authService.decodedAccessToken = this.jwtHelper.decodeToken(
        accessToken
      );
      this.userService.getSelf().subscribe((self: User) => {
        this.authService.currentUser = self;
        this.authService.changeProfilePhotoUrl(self.photoUrl);
      });
    }
  }

  isLoggedIn() {
    return this.authService.loggedIn();
  }
}
