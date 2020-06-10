import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/public_api';
import { Router } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() closeRegister = new EventEmitter();
  registerForm: FormGroup;
  user: User;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    };
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group(
      {
        gender: ['male'],
        username: ['', Validators.required],
        knownAs: ['', Validators.required],
        dateOfBirth: [null, Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(10)
          ]
        ],
        confirmPassword: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(10)
          ]
        ]
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(fg: FormGroup) {
    return fg.get('password').value === fg.get('confirmPassword').value
      ? null
      : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.registerForm.value).subscribe(
        () => {
          this.authService.login(this.user).subscribe(() => {
            this.router.navigate(['/matches']);
          });
        },
        err => {
          console.log(err);
        }
      );
    }
  }

  cancel() {
    this.closeRegister.emit();
  }
}
