import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {
  registerMode = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  toggleRegister() {
    this.registerMode = !this.registerMode;
  }

}
