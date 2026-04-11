import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../shared/Models/ResetPassword';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordData: ResetPassword = new ResetPassword();
  formGroup: FormGroup;

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _fb: FormBuilder,
    private _identityService: IdentityService,
    private _route: Router,
    private _toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe(params => {
      this.resetPasswordData.email = params['email'];
      this.resetPasswordData.token = params['code'];
    });
    this.FormValidation();
  }

  FormValidation() {
    this.formGroup = this._fb.group({
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/^(?=.*[A-Z])(?=.*\d).+$/)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/^(?=.*[A-Z])(?=.*\d).+$/)]]
    }, { Validators: this.PasswordMatchValidator });
  }

  PasswordMatchValidator(group: FormGroup) {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');

    if (password.value === confirmPassword.value) {
      this._confirmPassword.setErrors(null);
    } else {
      this._confirmPassword.setErrors({ passwordMismatch: true });
    }
  }

  get _password() {
    return this.formGroup.get('password');
  }
  get _confirmPassword() {
    return this.formGroup.get('confirmPassword');
  }

  onSubmit() {
    if (this.formGroup.valid) {
      this.resetPasswordData.password = this.formGroup.value.password;
      this._identityService.ResetPassword(this.resetPasswordData).subscribe({
        next: (response) => {
          console.log(response);
          this._toastr.success("Password reset successful. Please login with your new password.");
          this._route.navigateByUrl("/account/login");
        },
        error: (error) => {
          console.log(error);
          this._toastr.error("Failed to reset password.");
        }
      });

    }
  }
}
