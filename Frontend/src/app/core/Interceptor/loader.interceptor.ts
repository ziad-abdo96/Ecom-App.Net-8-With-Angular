// import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
// import { LoadingService } from '../Services/loading.service';
// import { delay, finalize, Observable } from 'rxjs';
// import { Injectable } from '@angular/core';


// @Injectable()
// export class loaderInterceptor implements HttpInterceptor {
//   constructor(private _loadingService: LoadingService) {

//   }

//   intercept(req: HttpRequest<any>, next: HttpHandler) :
//   Observable<HttpEvent<any>> {
//     this._loadingService.Loading();
//     return next.handle(req).pipe(

//      finalize(() => this._loadingService.HideLoader())
//     );
//   }
// }
