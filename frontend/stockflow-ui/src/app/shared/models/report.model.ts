// Summary data shown in report dashboard/cards.
// Mirrors InventorySummaryDto from backend.
export interface InventorySummary {
  totalProducts: number;
  totalQuantity: number;
  averagePrice: number;
  totalInventoryValue: number;
  lowStockProducts: number;
}

// Represents a single row in inventory report table.
// Mirrors InventoryReportItemDto from backend.
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
