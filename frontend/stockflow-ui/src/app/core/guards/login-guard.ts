import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

// This guard prevents logged-in users from opening login page again
export const loginGuard: CanActivateFn = () => {
  const router = inject(Router);
  const authService = inject(AuthService);

  // If already logged in, redirect to products page
  if (authService.isLoggedIn()) {
    router.navigate(['/products']);
    return false;
  }

  // If not logged in, allow login page
  return true;
};
