import { Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { IPagination } from '../shared/Models/Pagnation';
import { IProduct } from '../shared/Models/Product';
import { ICategory } from '../shared/Models/Category';
import { ProductParam } from '../shared/Models/ProductParam';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  product: IProduct[];
  category: ICategory[];
  totalCount: number;
  productParam = new ProductParam();
  @ViewChild('search') searchInput: ElementRef;
  @ViewChild('SortSelect') SortSelect: ElementRef;  

  constructor(private shopService: ShopService, private _toastrService: ToastrService) { 

  }
  ngOnInit(): void {
    this._toastrService.success('Test message');
    this.productParam.sortSelected = this.SortingOptions[0].value;
    this.GetAllProducts();
    this.GetCategories();
  }

  GetAllProducts(){
    this.shopService.GetProduct(this.productParam).subscribe({
      next: ((value: IPagination) => { 
        this.product = value.data;
        this.totalCount = value.totalCount;
        this.productParam.pageNumber = value.pageNumber;
        this.productParam.pageSize = value.pageSize;
        this._toastrService.success('Products loaded successfully', "Success");
      })
    })
  }

  GetCategories(){
    this.shopService.GetCategories().subscribe({
      next: ((value) => { 
        this.category = value;
      })
     })
  }

  OnchangePage(event: any) {
    this.productParam.pageNumber = event;
    this.GetAllProducts();
  }

  SelectedId(id: number) {
    this.productParam.categoryId = id;
    this.GetAllProducts();
  }

  SortingOptions = [
    { name: 'Price', value: 'Name'},
    { name: 'Price:min-max', value: 'PriceAce'},
    { name: 'Price:max-min', value: 'PriceDesc'},
  ]

  SortingByPrice(sort: Event) {
    this.productParam.sortSelected = (sort.target as HTMLInputElement).value;
    this.GetAllProducts();
  }

  OnSearch(search: string) {
    this.productParam.search = search;
    this.GetAllProducts();
  }

  ResetValue() {
    this.productParam.search = '';
    this.productParam.sortSelected = '';
    this.productParam.categoryId = 0;
    this.productParam.pageNumber = 1;
    this.GetAllProducts();
    this.searchInput.nativeElement.value = '';
    this.SortSelect.nativeElement.selectedIndex = 0;
  }
}
