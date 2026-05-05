// Health response data model
// Represents the "data" object returned from /api/health endpoint
export interface HealthStatus {
  // API health status (controlled values)
  status: 'Healthy' | 'Unhealthy' | 'Degraded';

  // Application/service name
  application: string;

  // ISO timestamp string returned from backend
  // Convert to Date in UI if needed
  timestamp: string;
}
