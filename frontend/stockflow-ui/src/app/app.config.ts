import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';

// Interceptors
import { authInterceptor } from './core/interceptors/auth-interceptor';
import { errorInterceptor } from './core/interceptors/error.interceptor';

// Central application configuration.
// Registers routing, HTTP client, and global interceptors.
export const appConfig: ApplicationConfig = {
  providers: [
    // Global Angular runtime error handling
    provideBrowserGlobalErrorListeners(),

    // Application routing configuration
    provideRouter(routes),

    // HttpClient with interceptors
    // Execution order:
    // Request → top to bottom
    // Response/Error → bottom to top
    provideHttpClient(
      withInterceptors([
        authInterceptor,   // attaches JWT token to outgoing requests
        errorInterceptor   // handles API errors globally
      ])
    )
  ]
};
