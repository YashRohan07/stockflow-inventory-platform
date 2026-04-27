// Health response data model
// This represents the data returned from /api/health
export interface HealthStatus {
  // Backend status, example: "Healthy"
  status: string;

  // Application name, example: "StockFlow API"
  application: string;

  // Response timestamp from backend
  timestamp: string;
}
