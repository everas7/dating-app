import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  photoUrl: string;
  constructor(public authService: AuthService) {}

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(
      photoUrl => (this.photoUrl = photoUrl)
    );
  }

  logout() {
    localStorage.removeItem('jwt');
    this.authService.currentUser = null;
  }
}
