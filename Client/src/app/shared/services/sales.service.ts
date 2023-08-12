import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from '../models/apiResponse';
import { ISalesBriefCreate } from '../models/salesBriefCreate';
import { IProductWiseSalesSummary, ISalesBrief } from '../models/salesBrief';
import { ICustomerSelect } from '../models/customerSelect';
import { IProductSelect } from '../models/productSelect';

@Injectable({
  providedIn: 'root',
})
export class SalesService {
  constructor(private http: HttpClient) {}

  getSales() {
    return this.http.get<IApiResponse<IProductWiseSalesSummary[]>>(
      'https://localhost:7061/api/sales/GetAllSales'
    );
  }

  createSale(sales: ISalesBriefCreate) {
    return this.http.post<IApiResponse<ISalesBrief>>(
      'https://localhost:7061/api/sales',
      sales
    );
  }

  getCustomerSelectList() {
    return this.http.get<IApiResponse<ICustomerSelect[]>>(
      'https://localhost:7061/api/sales/customerSelectList'
    );
  }

  getProductSelectList() {
    return this.http.get<IApiResponse<IProductSelect[]>>(
      'https://localhost:7061/api/sales/productSelectList'
    );
  }
}
