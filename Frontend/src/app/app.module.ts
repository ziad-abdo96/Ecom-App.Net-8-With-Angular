import { Injectable, NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ShopModule } from "./shop/shop.module";
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { NgxImageZoomModule } from "ngx-image-zoom";
import { NgxSpinnerModule } from "ngx-spinner";
//import { loaderInterceptor } from './core/Interceptor/loader.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    ProductDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    PaginationModule.forRoot(),
    NgxImageZoomModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      closeButton: true,
      positionClass: 'toast-top-right',
      countDuplicates: true,
      timeOut: 1500,
      progressBar: true,
    })
],
  providers: [
    provideClientHydration(),
    provideHttpClient(withInterceptorsFromDi()),
     provideAnimations(),
   // { provide: HTTP_INTERCEPTORS, useClass: loaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
