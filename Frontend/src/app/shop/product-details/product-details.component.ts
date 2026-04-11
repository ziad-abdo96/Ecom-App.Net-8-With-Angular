import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { IProduct } from '../../shared/Models/Product';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;
  mainImage: string;
  baseUrl = environment.baseUrl;
  baseUrlForImages = environment.baseUrlForImages;
  quantity : number = 1;

  constructor(
     private _shopService: ShopService,
     private _activatedRoute: ActivatedRoute,
     private _toastrService: ToastrService,
     private _basketService: BasketService
    ) 
     { }

  ngOnInit(): void {
    this.GetProductDetails();
  }


  GetProductDetails() {
    let id = +this._activatedRoute.snapshot.paramMap.get('id');
    this._shopService.GetProductById(id).subscribe({
      next: (response) => {
        this.product = response;
        this.mainImage = this.product.photos[0].imageName;
      },
      error: error => {
      }
    })
  }

  ReplaceImage(src: string) {
    this.mainImage = src;
  }

  IncrementBasket() {
    if(this.quantity < 10) {
      this.quantity++;
      this._toastrService.success("Item added to basket", "SUCCESS");
    } else {
      this._toastrService.error("you can't add items more than 10", "Enough");
    }

  }

  DecrementBasket() {
    if(this.quantity > 1) {
      this.quantity--;
      this._toastrService.warning("Item has been decrement", "SUCCESS");
    } else {
      this._toastrService.error("you can't decrement less than 1 ", "Error")
    }
  }

  AddToBasket() {
    this._basketService.AddItemToBasket(this.product, this.quantity);
  }

  CalculateDiscount(oldPrice, newPrice): number {
    return parseFloat(
      Math.round(((oldPrice - newPrice) / oldPrice)* 100).toFixed(1)
    )
  }
}

