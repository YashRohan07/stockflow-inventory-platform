import { Routes } from '@angular/router';

import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { ProductList } from './features/products/product-list/product-list';
import { Reports } from './features/reports/reports/reports';
import { LoginComponent } from './features/auth/login/login';

import { authGuard } from './core/guards/auth-guard';
import { loginGuard } from './core/guards/login-guard';

// Application routes
export const routes: Routes = [
  {
    // Root URL redirects to login
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    // Public login route
    // If user is already logged in, loginGuard redirects to dashboard
    path: 'login',
    component: LoginComponent,
    canActivate: [loginGuard]
  },
  {
    // Protected dashboard route
    path: 'dashboard',
    component: Dashboard,
    canActivate: [authGuard]
  },
  {
    // Protected products route
    path: 'products',
    component: ProductList,
    canActivate: [authGuard]
  },
  {
    // Protected reports route
    path: 'reports',
    component: Reports,
    canActivate: [authGuard]
  }
];
