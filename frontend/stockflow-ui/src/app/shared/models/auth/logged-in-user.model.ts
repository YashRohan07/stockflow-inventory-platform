// Safe logged-in user information returned after successful authentication.
// Contains only non-sensitive data needed for UI display and role-based behavior.
export interface LoggedInUser {
  id: number;
  name: string;
  email: string;

  // User role used for frontend access control and conditional UI rendering.
  role: 'Admin' | 'Member';
}
