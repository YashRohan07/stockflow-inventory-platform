// This model represents product data received from the backend API.
export interface Product {
  id: number;
  sku: string;
  name: string;
  size?: string | null;
  color?: string | null;
  quantity: number;
  purchasePrice: number;
  purchaseDate: string;
}

// This model is used when creating a new product.
export interface CreateProductRequest {
  sku: string;
  name: string;
  size?: string | null;
  color?: string | null;
  quantity: number;
  purchasePrice: number;
  purchaseDate: string;
}

// This model is used when updating an existing product.
// SKU is not included because SKU should not be changed after creation.
export interface UpdateProductRequest {
  name: string;
  size?: string | null;
  color?: string | null;
  quantity: number;
  purchasePrice: number;
  purchaseDate: string;
}

// This model represents the common API response format from backend.
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}
