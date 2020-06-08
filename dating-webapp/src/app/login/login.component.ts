import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @Output() goRegister = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(
      () => {
        console.log('Successfully logged in!');
      },
      err => {
        console.log('Login error', err);
      },
      () => {
        this.router.navigate(['/']);
      },
    );
  }


  register() {
    this.goRegister.emit();
  }
}
