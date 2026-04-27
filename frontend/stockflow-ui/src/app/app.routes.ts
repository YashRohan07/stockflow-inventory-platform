// Import Routes type from Angular Router
import { Routes } from '@angular/router';

// Import page components
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { ProductList } from './features/products/product-list/product-list';
import { Reports } from './features/reports/reports/reports';
import { Login } from './features/auth/login/login';

// Define all application routes here
export const routes: Routes = [
  {
    // Empty path means root URL: http://localhost:4200
    path: '',

    // Redirect root URL to dashboard page
    redirectTo: 'dashboard',

    // Match only the full empty path
    pathMatch: 'full'
  },
  {
    // Dashboard page route
    path: 'dashboard',
    component: Dashboard
  },
  {
    // Products page route
    path: 'products',
    component: ProductList
  },
  {
    // Reports page route
    path: 'reports',
    component: Reports
  },
  {
    // Login page route
    path: 'login',
    component: Login
  }
];
