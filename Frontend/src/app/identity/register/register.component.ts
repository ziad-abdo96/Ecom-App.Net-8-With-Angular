import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { validate } from 'uuid';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  formGroup: FormGroup;
  constructor(
    private _formBuilder: FormBuilder, 
    private _identityService: IdentityService, 
    private _toastrService: ToastrService,
    private _route: Router
  ) {}

  ngOnInit() {
    this.formValidation();
  }

  formValidation() {
    this.formGroup = this._formBuilder.group({
      UserName: ['', [Validators.required, Validators.minLength(3)]],
      Email: ['', [Validators.required, Validators.email]],
      DisplayName: ['', [Validators.required]],
      Password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/^(?=.*[A-Z])(?=.*\d).+$/)]],
    });
  }
  get _UserName() {
    return this.formGroup.get('UserName');
  }
  get _email() {
    return this.formGroup.get('Email');
  } 
  get _DisplayName() {
    return this.formGroup.get('DisplayName');
  }
  get _password() {
    return this.formGroup.get('Password');
  }

  onSubmit() {

    if (this.formGroup.valid) {
      this._identityService.Register(this.formGroup.value).subscribe({
        next:(value)=> {
          console.log(value);
          this._toastrService.success('Registration successful, Please activate your account.', 'success'.toUpperCase());
          this._route.navigateByUrl("/account/login");
        },
        error:(err: any)=> {
          console.log(err);
          this._toastrService.error(err.error.message, 'error'.toUpperCase());
        } 
      });
    }
  }
}