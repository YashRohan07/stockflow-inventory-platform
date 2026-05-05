import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse, LoggedInUser } from '../../shared/models/auth';

// Handles authentication state and login/logout operations.
// Stores JWT session data for frontend route guards and API authorization.
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseUrl = `${environment.apiBaseUrl}/Auth`;

  constructor(private readonly http: HttpClient) { }

  // Sends login credentials to backend and stores session data after successful login.
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, request)
      .pipe(
        tap(response => {
          this.setSession(response);
        })
      );
  }

  // Stores authentication data in localStorage.
  // Note: localStorage is simple for learning/demo projects, but HTTP-only cookies are safer for production.
  private setSession(response: LoginResponse): void {
    localStorage.setItem('token', response.token);
    localStorage.setItem('user', JSON.stringify(response.user));
    localStorage.setItem('expiresAt', response.expiresAt);
  }

  // Returns JWT token used by HTTP interceptor.
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Returns logged-in user information for UI display and role-based rendering.
  getUser(): LoggedInUser | null {
    const user = localStorage.getItem('user');

    return user ? JSON.parse(user) as LoggedInUser : null;
  }

  // Returns current user role for frontend access control.
  getUserRole(): 'Admin' | 'Member' | null {
    return this.getUser()?.role ?? null;
  }

  // Checks whether a valid non-expired session exists.
  // Expired sessions are cleared immediately to prevent stale frontend state.
  isLoggedIn(): boolean {
    const token = this.getToken();
    const expiresAt = localStorage.getItem('expiresAt');

    if (!token || !expiresAt) {
      return false;
    }

    const expiryTime = new Date(expiresAt).getTime();
    const currentTime = Date.now();

    if (currentTime >= expiryTime) {
      this.logout();
      return false;
    }

    return true;
  }

  // Clears authentication data during logout or token expiry.
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('expiresAt');
  }
}
