import { LoggedInUser } from './logged-in-user.model';

// Backend response after successful login
export interface LoginResponse {
  token: string;
  expiresAt: string;
  user: LoggedInUser;
}
