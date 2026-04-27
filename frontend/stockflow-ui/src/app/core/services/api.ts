// Import Injectable to create Angular service
import { Injectable } from '@angular/core';

// Import HttpClient to make HTTP requests
import { HttpClient } from '@angular/common/http';

// Import Observable because HTTP requests return Observable data
import { Observable } from 'rxjs';

// Import environment to get API base URL
import { environment } from '../../../environments/environment';

// This service will handle common API requests
@Injectable({
  providedIn: 'root'
})
export class Api {
  // Store base API URL from environment file
  private readonly baseUrl = environment.apiBaseUrl;

  // Inject HttpClient so we can call backend APIs
  constructor(private readonly http: HttpClient) { }

  // Generic GET request method
  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
  }

  // Generic POST request method
  post<T>(endpoint: string, body: unknown): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  // Generic PUT request method
  put<T>(endpoint: string, body: unknown): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  // Generic DELETE request method
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
  }
}
