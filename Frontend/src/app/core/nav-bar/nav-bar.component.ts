import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {
  count: number = 0;
  constructor(private _basketService: BasketService) {
    
  }


  ngOnInit() {
  const basketId = localStorage.getItem('basketId'); 

    this._basketService.GetBasket(basketId).subscribe();

  this._basketService.basket$.subscribe({
    next: (basket) => {
      if (basket) 
        this.count = basket.basketItems.length;
    }
  });
}

}
