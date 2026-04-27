// Common API response model
// This matches the backend ApiResponse<T> structure
export interface ApiResponse<T> {
  // true means request was successful, false means failed
  success: boolean;

  // message from backend
  message: string;

  // actual response data
  data: T | null;

  // error details if any
  errors: unknown;
}
