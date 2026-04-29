import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { finalize } from 'rxjs';

import { Product } from '../../../shared/models/product.model';
import { ProductService } from '../product.service';

// This standalone component shows the product list.
@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule, // Needed for *ngIf, *ngFor, date pipe
    RouterModule  // Needed for routerLink
  ],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  isLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private productService: ProductService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  // Load all products from backend API.
  loadProducts(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.productService.getAllProducts()
      .pipe(
        finalize(() => {
          this.isLoading = false;

          // Force UI update after async API call.
          // This fixes the issue where data appears only after second click.
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.products = response.data ?? [];
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('Product load failed:', error);
          this.errorMessage = 'Failed to load products.';
          this.products = [];
          this.cdr.detectChanges();
        }
      });
  }

  // Delete product and reload list.
  deleteProduct(id: number): void {
    const confirmed = confirm('Are you sure you want to delete this product?');

    if (!confirmed) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';

    this.productService.deleteProduct(id).subscribe({
      next: () => {
        this.successMessage = 'Product deleted successfully.';
        this.loadProducts();
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Delete failed:', error);
        this.errorMessage = 'Failed to delete product.';
        this.cdr.detectChanges();
      }
    });
  }
}
