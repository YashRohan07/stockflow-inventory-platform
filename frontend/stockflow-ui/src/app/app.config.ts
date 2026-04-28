// Import ApplicationConfig to configure the Angular app
// Import provideBrowserGlobalErrorListeners to handle global errors
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

// Import provideRouter to enable routing in the app
import { provideRouter } from '@angular/router';

// Import HttpClient + interceptor support
import { provideHttpClient, withInterceptors } from '@angular/common/http';

// Import routes
import { routes } from './app.routes';

// Import our custom Auth Interceptor
import { authInterceptor } from './core/interceptors/auth-interceptor';

// Main application configuration
export const appConfig: ApplicationConfig = {
  providers: [
    // Global error handling
    provideBrowserGlobalErrorListeners(),

    // Enable routing
    provideRouter(routes),

    // Enable HttpClient + attach interceptor
    // This ensures every API call automatically includes JWT token
    provideHttpClient(
      withInterceptors([authInterceptor])
    )
  ]
};
