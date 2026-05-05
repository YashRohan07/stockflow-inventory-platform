import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';

// Entry point of the Angular application.
// Bootstraps the root App component with global configuration.
bootstrapApplication(App, appConfig)
  .catch((err) => {
    // Log bootstrap failure (useful for debugging startup issues)
    console.error('Application bootstrap failed:', err);
  });
