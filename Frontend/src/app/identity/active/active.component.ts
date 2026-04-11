import { Component, OnInit } from '@angular/core';
import { ActiveAccount } from '../../shared/Models/ActiveAccount';
import { ActivatedRoute, Router } from '@angular/router';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs';

@Component({
  selector: 'app-active',
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent implements OnInit {
  activeParam = new ActiveAccount();

  constructor(
    private _activateRoute: ActivatedRoute,
    private _identityService: IdentityService,
    private _toastrService: ToastrService,
    private _route: Router
  ) { }

  ngOnInit(): void {
    this._activateRoute.queryParams.pipe(first()).subscribe(params => {
      if (!params['email'] || !params['code']) {
        this._toastrService.error("Invalid activation link.");
        return;
      }
      this.activeParam.email = params['email'];
      this.activeParam.token = params['code'];
      this._identityService.Active(this.activeParam).subscribe({
        next: (value) => {
          console.log(value);
          this._toastrService.success("Your account has been activated successfully.");
          this._route.navigateByUrl("/account/login");
        },
        error: (err) => {
          console.error(err);
          this._toastrService.error("Your account activation failed, token is expired or invalid.");
        }
      });
    });
  }

}

