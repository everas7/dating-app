import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/public_api';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() closeRegister = new EventEmitter();
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private fb: FormBuilder) {}

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
    // this.authService.register(this.model).subscribe(
    //   () => {
    //     this.closeRegister.emit();
    //   },
    //   err => {
    //     console.log(err);
    //   }
    // );
    console.log(this.registerForm.value);
  }

  cancel() {
    this.closeRegister.emit();
  }
}
