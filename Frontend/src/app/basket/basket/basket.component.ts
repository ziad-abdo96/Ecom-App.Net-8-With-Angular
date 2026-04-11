import { Component, OnInit } from '@angular/core';
import { BasketService } from '../basket.service';
import { IBasket, IBasketItem } from '../../shared/Models/Basket';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent implements OnInit {

  basket: IBasket;
  baseURL = environment.baseUrl;
  baseUrlForImages = environment.baseUrlForImages;
  constructor(private _basketService: BasketService) {
  }
  ngOnInit(): void {
    this._basketService.basket$.subscribe({
      next:(value) => {
        this.basket = value;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  RemoveBasket(item: IBasketItem) {
    this._basketService.RemoveItemFromBasket(item);
  }

  IncrementQuantity(item: IBasketItem) {
    this._basketService.IncrementBasketItemQuantity(item);
  }

  DecrementQuantity(item: IBasketItem) {
    this._basketService.DecrementBasketItemQuantity(item);
  }
}
