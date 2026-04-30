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

// Query parameters for product list search/filter/sort/pagination.
export interface ProductQueryParameters {
  search?: string;
  purchaseDateFrom?: string;
  purchaseDateTo?: string;
  sortBy?: string;
  sortOrder?: string;
  page: number;
  pageSize: number;
}

// Generic paged response from backend.
export interface PagedResponse<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
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
