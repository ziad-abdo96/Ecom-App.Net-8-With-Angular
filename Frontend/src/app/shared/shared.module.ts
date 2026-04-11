import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationComponent } from './Component/pagination/pagination.component';
import { OrderTotalComponent } from "./Component/order-total/order-total.component";

@NgModule({
    declarations: [
    PaginationComponent,
    OrderTotalComponent
  ],
    imports: [
        CommonModule,
        PaginationModule.forRoot(),
        
    ], exports: [
        PaginationModule,
        PaginationComponent, 
        OrderTotalComponent
    ]
})
export class SharedModule { }