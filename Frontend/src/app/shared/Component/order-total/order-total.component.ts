import { Component, OnInit } from '@angular/core';
import { IBasketTotal } from '../../Models/Basket';
import { BasketService } from '../../../basket/basket.service';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrl: './order-total.component.scss'
})
export class OrderTotalComponent implements OnInit {
  basketTotals: IBasketTotal;

  constructor(private _basketService: BasketService) {}
  
  ngOnInit(): void {
    this._basketService.basketTotal$.subscribe({
      next: (value) => {
        this.basketTotals = value;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

}
