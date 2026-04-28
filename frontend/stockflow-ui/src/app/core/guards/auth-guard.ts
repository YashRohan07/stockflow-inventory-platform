import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

// This guard protects private routes
export const authGuard: CanActivateFn = () => {
  const router = inject(Router);
  const authService = inject(AuthService);

  // Allow route access only if user is logged in
  if (authService.isLoggedIn()) {
    return true;
  }

  // If not logged in, redirect to login page
  router.navigate(['/login']);
  return false;
};
