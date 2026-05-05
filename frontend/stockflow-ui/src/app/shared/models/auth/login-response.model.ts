import { LoggedInUser } from './logged-in-user.model';

// Response returned from backend after successful authentication.
// Mirrors LoginResponseDto from ASP.NET Core backend.
export interface LoginResponse {
  // JWT token used for authenticated API requests
  token: string;

  // Token expiration time (ISO string from backend)
  // Convert to Date when performing time comparisons
  expiresAt: string;

  // Safe user information for UI and role-based access
  user: LoggedInUser;
}
