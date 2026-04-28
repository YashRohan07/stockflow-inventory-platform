// Safe logged-in user information returned from backend
export interface LoggedInUser {
  id: number;
  name: string;
  email: string;
  role: 'Admin' | 'Member';
}
