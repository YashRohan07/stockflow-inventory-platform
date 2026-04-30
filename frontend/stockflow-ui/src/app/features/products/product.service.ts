import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  ApiResponse,
  CreateProductRequest,
  Product,
  UpdateProductRequest,
  ProductQueryParameters,
  PagedResponse
} from '../../shared/models/product.model';

// This service handles API calls related to products.
// Components should not call HttpClient directly.
@Injectable({
  providedIn: 'root'
})
export class ProductService {

  // Backend API base URL for products
  private readonly apiUrl = 'http://localhost:5118/api/products';

  constructor(private http: HttpClient) { }

  // Get products with filters, sorting, pagination
  getAllProducts(params: ProductQueryParameters): Observable<ApiResponse<PagedResponse<Product>>> {

    const queryParams: string[] = [];

    if (params.search) {
      queryParams.push(`search=${encodeURIComponent(params.search)}`);
    }

    if (params.purchaseDateFrom) {
      queryParams.push(`purchaseDateFrom=${params.purchaseDateFrom}`);
    }

    if (params.purchaseDateTo) {
      queryParams.push(`purchaseDateTo=${params.purchaseDateTo}`);
    }

    if (params.sortBy) {
      queryParams.push(`sortBy=${params.sortBy}`);
    }

    if (params.sortOrder) {
      queryParams.push(`sortOrder=${params.sortOrder}`);
    }

    if (params.page) {
      queryParams.push(`page=${params.page}`);
    }

    if (params.pageSize) {
      queryParams.push(`pageSize=${params.pageSize}`);
    }

    const queryString = queryParams.length > 0
      ? `?${queryParams.join('&')}`
      : '';

    return this.http.get<ApiResponse<PagedResponse<Product>>>(`${this.apiUrl}${queryString}`);
  }

  // Get one product by Id
  getProductById(id: number): Observable<ApiResponse<Product>> {
    return this.http.get<ApiResponse<Product>>(`${this.apiUrl}/${id}`);
  }

  // Create a new product
  createProduct(request: CreateProductRequest): Observable<ApiResponse<Product>> {
    return this.http.post<ApiResponse<Product>>(this.apiUrl, request);
  }

  // Update an existing product
  updateProduct(id: number, request: UpdateProductRequest): Observable<ApiResponse<Product>> {
    return this.http.put<ApiResponse<Product>>(`${this.apiUrl}/${id}`, request);
  }

  // Delete product (UPDATED)
  deleteProduct(id: number): Observable<ApiResponse<unknown>> {
    return this.http.delete<ApiResponse<unknown>>(`${this.apiUrl}/${id}`);
  }
}
