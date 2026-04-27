// Import ApplicationConfig to configure the Angular app
// Import provideBrowserGlobalErrorListeners to handle global errors
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

// Import provideRouter to enable routing in the app
import { provideRouter } from '@angular/router';

// Import provideHttpClient to enable HTTP API calls
import { provideHttpClient } from '@angular/common/http';

// Import routes (defined in app.routes.ts)
import { routes } from './app.routes';

// Main application configuration
export const appConfig: ApplicationConfig = {
  providers: [
    // Handle global errors across the application
    provideBrowserGlobalErrorListeners(),

    // Enable routing (navigation between pages)
    provideRouter(routes),

    // Enable HttpClient for calling backend APIs
    provideHttpClient()
  ]
};
