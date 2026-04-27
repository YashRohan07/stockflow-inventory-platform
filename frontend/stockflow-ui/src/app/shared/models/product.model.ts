// Product model
// This represents one product item in StockFlow
export interface Product {
  // Unique product ID from database
  id: number;

  // Stock Keeping Unit - unique product code
  sku: string;

  // Product name
  name: string;

  // Product size, example: S, M, L, XL
  size: string;

  // Product color
  color: string;

  // Available stock quantity
  quantity: number;

  // Product purchase price
  purchasePrice: number;

  // Product purchase date
  purchaseDate: string;

  // Record created date
  createdAt: string;

  // Record updated date, can be null
  updatedAt: string | null;
}
