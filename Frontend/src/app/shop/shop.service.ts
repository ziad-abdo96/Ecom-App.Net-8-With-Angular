import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/Pagnation';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ICategory } from '../shared/Models/Category';
import { ProductParam } from '../shared/Models/ProductParam';
import { IProduct } from '../shared/Models/Product';

@Injectable({
  providedIn: 'root'
})

export class ShopService {

   baseUrl='https://localhost:44350/api/'; 
  constructor(private _httpClient: HttpClient) { }
  
   GetProduct(productParams: ProductParam) {
    let param = new HttpParams();
    if(productParams.categoryId) {
      param = param.append('categoryId', productParams.categoryId); 
    }
    if(productParams.search) {
      param = param.append('search', productParams.search); 
    }
    if(productParams.sortSelected) {
      param = param.append('sort', productParams.sortSelected); 
    }
    param = param.append('pageNumber', productParams.pageNumber); 
    param = param.append('pageSize', productParams.pageSize); 
    return this._httpClient.get<IPagination>(this.baseUrl + "Products/get-all", {params: param});
  }

  GetProductById(id: number) {
    return this._httpClient.get<IProduct>(this.baseUrl + "Products/get-by-id/" + id);
  }

  GetCategories() {
    return this._httpClient.get<ICategory[]>(this.baseUrl + "Categories/get-all");  
  }
}
