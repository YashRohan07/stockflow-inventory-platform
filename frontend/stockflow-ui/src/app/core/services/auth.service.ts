import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { LoginRequest, LoginResponse, LoggedInUser } from '../../shared/models/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Backend Auth API base URL
  private baseUrl = 'http://localhost:5118/api/Auth';

  constructor(private http: HttpClient) { }

  // Send login request to backend
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, request)
      .pipe(
        tap(response => {
          // Save token/user after successful login
          this.setSession(response);
        })
      );
  }

  // Save login data in localStorage
  private setSession(response: LoginResponse): void {
    localStorage.setItem('token', response.token);
    localStorage.setItem('user', JSON.stringify(response.user));
    localStorage.setItem('expiresAt', response.expiresAt);
  }

  // Get JWT token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Get logged-in user info
  getUser(): LoggedInUser | null {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) as LoggedInUser : null;
  }

  // Get logged-in user role
  getUserRole(): 'Admin' | 'Member' | null {
    return this.getUser()?.role ?? null;
  }

  // Check if user is logged in and token is not expired
  isLoggedIn(): boolean {
    const token = this.getToken();
    const expiresAt = localStorage.getItem('expiresAt');

    // No token or expiry means user is not logged in
    if (!token || !expiresAt) {
      return false;
    }

    const expiryTime = new Date(expiresAt).getTime();
    const currentTime = new Date().getTime();

    // If token expired, remove session
    if (currentTime >= expiryTime) {
      this.logout();
      return false;
    }

    return true;
  }

  // Clear login data
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('expiresAt');
  }
}
