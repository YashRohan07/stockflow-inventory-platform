import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { finalize } from 'rxjs';

import {
  PagedResponse,
  Product,
  ProductQueryParameters
} from '../../../shared/models/product.model';

import { ProductService } from '../product.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  isLoading = false;
  errorMessage = '';
  successMessage = '';

  search = '';
  purchaseDateFrom = '';
  purchaseDateTo = '';
  sortBy = 'purchaseDate';
  sortOrder = 'desc';

  page = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  hasPreviousPage = false;
  hasNextPage = false;

  showDeleteModal = false;
  selectedProductId: number | null = null;
  selectedProductName = '';
  isDeleting = false;

  constructor(
    private readonly productService: ProductService,
    public readonly authService: AuthService,
    private readonly cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  get isAdmin(): boolean {
    return this.authService.getUserRole() === 'Admin';
  }

  private buildQuery(): ProductQueryParameters {
    return {
      search: this.search.trim() || undefined,
      purchaseDateFrom: this.purchaseDateFrom || undefined,
      purchaseDateTo: this.purchaseDateTo || undefined,
      sortBy: this.sortBy,
      sortOrder: this.sortOrder,
      page: this.page,
      pageSize: this.pageSize
    };
  }

  loadProducts(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.productService.getAllProducts(this.buildQuery())
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          const pagedData: PagedResponse<Product> | null = response.data ?? null;

          this.products = pagedData?.items ?? [];
          this.page = pagedData?.page ?? 1;
          this.pageSize = pagedData?.pageSize ?? 10;
          this.totalCount = pagedData?.totalCount ?? 0;
          this.totalPages = pagedData?.totalPages ?? 0;
          this.hasPreviousPage = pagedData?.hasPreviousPage ?? false;
          this.hasNextPage = pagedData?.hasNextPage ?? false;
        },
        error: (error) => {
          console.error('Product load failed:', error);
          this.errorMessage = 'Failed to load products.';
          this.products = [];
        }
      });
  }

  applyFilters(): void {
    this.page = 1;
    this.successMessage = '';
    this.errorMessage = '';
    this.loadProducts();
  }

  resetFilters(): void {
    this.search = '';
    this.purchaseDateFrom = '';
    this.purchaseDateTo = '';
    this.sortBy = 'purchaseDate';
    this.sortOrder = 'desc';
    this.page = 1;
    this.pageSize = 10;
    this.successMessage = '';
    this.errorMessage = '';

    this.loadProducts();
  }

  goToPreviousPage(): void {
    if (!this.hasPreviousPage || this.isLoading) {
      return;
    }

    this.page--;
    this.loadProducts();
  }

  goToNextPage(): void {
    if (!this.hasNextPage || this.isLoading) {
      return;
    }

    this.page++;
    this.loadProducts();
  }

  changePageSize(): void {
    this.page = 1;
    this.loadProducts();
  }

  getQuantityClass(quantity: number): string {
    if (quantity < 20) {
      return 'low';
    }

    if (quantity < 50) {
      return 'medium';
    }

    return 'high';
  }

  openDeleteModal(product: Product): void {
    if (!this.isAdmin) {
      this.errorMessage = 'You do not have permission to delete products.';
      this.successMessage = '';
      return;
    }

    this.selectedProductId = product.id;
    this.selectedProductName = product.name;
    this.showDeleteModal = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  closeDeleteModal(): void {
    if (this.isDeleting) {
      return;
    }

    this.resetDeleteModal();
  }

  private resetDeleteModal(): void {
    this.showDeleteModal = false;
    this.selectedProductId = null;
    this.selectedProductName = '';
  }

  confirmDelete(): void {
    if (this.selectedProductId === null) {
      return;
    }

    if (!this.isAdmin) {
      this.errorMessage = 'You do not have permission to delete products.';
      this.resetDeleteModal();
      return;
    }

    const productIdToDelete = this.selectedProductId;

    this.isDeleting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.productService.deleteProduct(productIdToDelete)
      .pipe(
        finalize(() => {
          this.isDeleting = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          this.resetDeleteModal();
          this.successMessage = 'Product deleted successfully.';
          this.loadProducts();
        },
        error: (error) => {
          console.error('Delete failed:', error);

          if (error.status === 403) {
            this.errorMessage = 'You do not have permission to delete products.';
          } else if (error.status === 401) {
            this.errorMessage = 'Session expired. Please login again.';
          } else if (error.status === 404) {
            this.errorMessage = 'Product not found. It may have already been deleted.';
          } else {
            this.errorMessage = 'Failed to delete product. Please try again.';
          }
        }
      });
  }
}
