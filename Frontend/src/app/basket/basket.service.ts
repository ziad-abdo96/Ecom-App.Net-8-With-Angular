import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/Models/Basket';
import { IProduct } from '../shared/Models/Product';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseURL = environment.baseUrl
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  private basketSourceTotal$ =  new BehaviorSubject<IBasketTotal>(null);
  basketTotal$ = this.basketSourceTotal$.asObservable();
  constructor(private _httpClient: HttpClient) {
  }

  CalculateTotal() {
    const basket = this.GetCurrentValue();
    const shipping = 0;
    const subTotal = basket.basketItems.reduce((a, c) => {
      return (c.price * c.quantity) + a
    }, 0)
    const total = shipping + subTotal;

    this.basketSourceTotal$.next({shipping, subTotal,total})
  }

  GetBasket(id: string) {
    return this._httpClient.get(this.baseURL + "baskets/get-basket-item/" + id).pipe(
      map((value: IBasket) => {
        this.basketSource.next(value);
        this.CalculateTotal();
        return value;
      })
    )
  }

  SetBasket(basket: IBasket) {
    return this._httpClient.post(this.baseURL + "Baskets/update-basket", basket).subscribe({
      next: (value: IBasket) => {
        this.basketSource.next(value);
        this.CalculateTotal();
      },
      error(err) {
        console.log(err);
      }
    })
  }

  GetCurrentValue() {
    return this.basketSource.value;
  }

  AddItemToBasket(product: IProduct, quantity: number = 1) {
    const basketItemToAdd: IBasketItem = this.MapProductToBasketItem(product, quantity);
    let basket = this.GetCurrentValue();

    if (!basket || !basket.id) {
      basket = this.CreateBasket();
    }

    basket.basketItems = this.AddOrUpdate(basket.basketItems, basketItemToAdd, quantity)

    return this.SetBasket(basket);
  }

  private AddOrUpdate(basketItems: IBasketItem[], basketItemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = basketItems.findIndex(i => i.id === basketItemToAdd.id);
    if (index == -1) {
      // basketItemToAdd.quantity = quantity;
      basketItems.push(basketItemToAdd);
    } else {
      basketItems[index].quantity += quantity;

    }

    return basketItems;
  }

  private CreateBasket(): IBasket {
    const basket = new Basket();
    console.log("Created basket id:", basket.id);
    localStorage.setItem("basketId", basket.id);
    return basket;
  }

  private MapProductToBasketItem(product: IProduct, quantity: number): IBasketItem {
    return {
      id: product.id,
      name: product.name,
      image: product.photos[0].imageName,
      description: product.description,
      quantity: quantity,
      price: product.newPrice,
      category: product.categoryName
    }
  }

  IncrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    const itemIndex = basket.basketItems.findIndex(i => i.id === item.id);
    basket.basketItems[itemIndex].quantity++;
    this.SetBasket(basket);
  }

  DecrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    const itemIndex = basket.basketItems.findIndex(i => i.id === item.id);
    if (basket.basketItems[itemIndex].quantity > 1) {
      basket.basketItems[itemIndex].quantity--;
    } else {
      this.RemoveItemFromBasket(item)
    }
  }

  RemoveItemFromBasket(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    if (basket.basketItems.some(i => i.id === item.id)) {
      basket.basketItems = basket.basketItems.filter(i => i.id !== item.id)
      if (basket.basketItems.length > 0) {
        this.SetBasket(basket)
      } else {
        this.DeleteBasketItem(basket)
      }
    }
  }
  DeleteBasketItem(basket: IBasket) {
    this._httpClient.delete(this.baseURL + "baskets/delete-basket/" + basket.id).subscribe({
      next: (value) => {
        this.basketSource.next(null);
        localStorage.removeItem('basketId');
      },
      error(err) {
        console.log(err);
      }
    })
  }

}
