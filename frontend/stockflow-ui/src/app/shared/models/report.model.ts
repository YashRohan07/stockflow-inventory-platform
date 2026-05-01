// Summary data shown in report cards.
export interface InventorySummary {
  totalProducts: number;
  totalQuantity: number;
  averagePrice: number;
  totalInventoryValue: number;
  lowStockProducts: number;
}

// One row inside inventory report table.
export interface InventoryReportItem {
  sku: string;
  name: string;
  size?: string | null;
  color?: string | null;
  quantity: number;
  purchasePrice: number;
  purchaseDate: string;
  totalValue: number;
}

// Common API response format.
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}
