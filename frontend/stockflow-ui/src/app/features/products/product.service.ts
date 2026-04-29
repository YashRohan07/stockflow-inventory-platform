import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  ApiResponse,
  CreateProductRequest,
  Product,
  UpdateProductRequest
} from '../../shared/models/product.model';

// This service handles API calls related to products.
// Components should not call HttpClient directly.
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  // Backend API base URL for products.
  private readonly apiUrl = 'http://localhost:5118/api/products';

  constructor(private http: HttpClient) { }

  // Get all products from backend.
  getAllProducts(): Observable<ApiResponse<Product[]>> {
    return this.http.get<ApiResponse<Product[]>>(this.apiUrl);
  }

  // Get one product by Id.
  getProductById(id: number): Observable<ApiResponse<Product>> {
    return this.http.get<ApiResponse<Product>>(`${this.apiUrl}/${id}`);
  }

  // Create a new product.
  createProduct(request: CreateProductRequest): Observable<ApiResponse<Product>> {
    return this.http.post<ApiResponse<Product>>(this.apiUrl, request);
  }

  // Update an existing product.
  updateProduct(id: number, request: UpdateProductRequest): Observable<ApiResponse<Product>> {
    return this.http.put<ApiResponse<Product>>(`${this.apiUrl}/${id}`, request);
  }

  // Delete an existing product.
  deleteProduct(id: number): Observable<ApiResponse<null>> {
    return this.http.delete<ApiResponse<null>>(`${this.apiUrl}/${id}`);
  }
}
