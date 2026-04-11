import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  requestCount = 0;
  constructor(private _service: NgxSpinnerService) {

   }

   Loading() {
    this.requestCount++;
    this._service.show(undefined, {
      bdColor: "rgba(0, 0, 0, 0.8)",
      size: "large",
      color: "#fff",
      type: "square-jelly-box",
      fullScreen: true,
    });
   }

   HideLoader() {
    this.requestCount--;
    if(this.requestCount <= 0) {
      this.requestCount = 0;
      this._service.hide();
    }
  }
}

