import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth-interceptor';

// Main application configuration
export const appConfig: ApplicationConfig = {
  providers: [
    // Global error handling
    provideBrowserGlobalErrorListeners(),

    // Enable routing
    provideRouter(routes),

    // Enable HttpClient + attach interceptor
    provideHttpClient(
      withInterceptors([authInterceptor])
    )
  ]
};
