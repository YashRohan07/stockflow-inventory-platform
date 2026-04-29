import { Routes } from '@angular/router';

import { ProductListComponent } from './features/products/product-list/product-list.component';
import { ProductFormComponent } from './features/products/product-form/product-form.component';
import { Reports } from './features/reports/reports/reports';
import { LoginComponent } from './features/auth/login/login';

import { authGuard } from './core/guards/auth-guard';
import { loginGuard } from './core/guards/login-guard';

// Application routes
export const routes: Routes = [
  {
    // Root URL redirects to login.
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    // Public login route.
    // If user is already logged in, loginGuard should redirect to products.
    path: 'login',
    component: LoginComponent,
    canActivate: [loginGuard]
  },
  {
    // Main protected page after login.
    // This is now the main dashboard-like inventory screen.
    path: 'products',
    component: ProductListComponent,
    canActivate: [authGuard]
  },
  {
    // Create product page.
    path: 'products/create',
    component: ProductFormComponent,
    canActivate: [authGuard]
  },
  {
    // Edit product page.
    path: 'products/edit/:id',
    component: ProductFormComponent,
    canActivate: [authGuard]
  },
  {
    // Optional reports page for future report generation.
    path: 'reports',
    component: Reports,
    canActivate: [authGuard]
  },
  {
    // Unknown URL redirects to products if logged in.
    path: '**',
    redirectTo: 'products'
  }
];
