// Payload sent from Angular login form to backend authentication API.
// Mirrors LoginRequestDto from backend.
export interface LoginRequest {
  // User email (should be normalized to lowercase before sending)
  email: string;

  // Plain-text password (will be validated and hashed on backend)
  password: string;
}
