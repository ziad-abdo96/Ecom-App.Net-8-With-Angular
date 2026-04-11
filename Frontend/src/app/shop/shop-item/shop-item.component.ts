import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/Models/Product';
import { BasketService } from '../../basket/basket.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.scss'
})
export class ShopItemComponent {
  constructor(private _basketService: BasketService) {
  }
@Input() product: IProduct;
baseUrl = environment.baseUrl;
baseUrlForImages = environment.baseUrlForImages;
SetBasketValue() {
  this._basketService.AddItemToBasket(this.product)
}
}
