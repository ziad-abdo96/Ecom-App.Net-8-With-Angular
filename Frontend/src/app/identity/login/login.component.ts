import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  formGroup: FormGroup;
  emailModule: string = '';   
z
  constructor(
    private _fb: FormBuilder,
    private _identityService: IdentityService,
    private _route: Router,
    private _toasterService: ToastrService
  ) { }

  ngOnInit(): void {
    this.FormValidation();
  }

  FormValidation() {
    this.formGroup = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/^(?=.*[A-Z])(?=.*\d).+$/)]],
    });
  }

  get _email() {
    return this.formGroup.get('email');
  }

  get _password() {
    return this.formGroup.get('password');
  }

  onSubmit() {
    if (this.formGroup.valid) {
      this._identityService.Login(this.formGroup.value).subscribe({
        next: (response) => {
          console.log(response);
          this._route.navigateByUrl("/");
        },
        error: (error) => {
          console.log(error);
        }
      });
    }
  }

  SendEmailForForgetPassword() {
    this._identityService.ForgetPassword(this.emailModule).subscribe({
      next: (response) => {

        console.log(response);
        this._toasterService.success("A password reset link has been sent to your email if it exists in our system.");
        
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
