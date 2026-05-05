import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

// Handles HTTP errors globally for API requests.
// Extracts backend error messages and provides a simple user-facing notification.
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error) => {
      // Default fallback message when backend does not provide a clear error response
      let message = 'Something went wrong';

      // Prefer standardized backend error message if available
      if (error.error?.message) {
        message = error.error.message;
      }

      // Simple global error display.
      // Replace with a toast/snackbar service in a production UI.
      alert(message);

      // Re-throw error so individual components can still handle it if needed.
      return throwError(() => error);
    })
  );
};
