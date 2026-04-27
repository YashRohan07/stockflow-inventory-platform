// Import Component and signal from Angular
import { Component, signal } from '@angular/core';

// Import Product model
import { Product } from '../../../shared/models/product.model';

// Product list page component
@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductList {
  // Temporary sample products
  // Later this data will come from backend API
  products = signal<Product[]>([
    {
      id: 1,
      sku: 'SF-001',
      name: 'Black T-Shirt',
      size: 'M',
      color: 'Black',
      quantity: 25,
      purchasePrice: 450,
      purchaseDate: '2026-04-27',
      createdAt: '2026-04-27',
      updatedAt: null
    },
    {
      id: 2,
      sku: 'SF-002',
      name: 'Blue Shirt',
      size: 'L',
      color: 'Blue',
      quantity: 15,
      purchasePrice: 700,
      purchaseDate: '2026-04-27',
      createdAt: '2026-04-27',
      updatedAt: null
    }
  ]);
}
