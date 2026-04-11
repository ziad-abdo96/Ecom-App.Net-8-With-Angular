import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ActiveAccount } from '../shared/Models/ActiveAccount';
import { ResetPassword } from '../shared/Models/ResetPassword';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  baseUrl = environment.baseUrl;

  constructor(private _httpClient : HttpClient) { }

  Register(model: any) {
    return this._httpClient.post(this.baseUrl + 'account/register', model);
  }

  Active(model: ActiveAccount) {
    return this._httpClient.post(this.baseUrl + 'Account/active-account', model);
  }

  Login(model: any) {
    return this._httpClient.post(this.baseUrl + 'account/login', model);
  }

  ForgetPassword(email: string) {
    return this._httpClient.get(this.baseUrl + 'Account/send-email-forget-password?email=' + email);  
  }

  ResetPassword(model: ResetPassword) {
   return this._httpClient.post(this.baseUrl + 'Account/reset-password', model);
  }


}
