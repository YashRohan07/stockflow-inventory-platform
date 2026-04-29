import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';

import {
  CreateProductRequest,
  UpdateProductRequest
} from '../../../shared/models/product.model';

import { ProductService } from '../product.service';

// This standalone component handles both create and edit product form.
@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,        // Needed for *ngIf
    ReactiveFormsModule, // Needed for formGroup
    RouterModule         // Needed for routerLink
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm!: FormGroup;

  isEditMode = false;
  productId!: number;

  isSubmitting = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.buildForm();

    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEditMode = true;
      this.productId = Number(id);
      this.loadProduct(this.productId);
    }
  }

  // Create reactive form with validation.
  buildForm(): void {
    this.productForm = this.fb.group({
      sku: ['', [Validators.required, Validators.maxLength(50)]],
      name: ['', [Validators.required, Validators.maxLength(150)]],
      size: ['', [Validators.maxLength(50)]],
      color: ['', [Validators.maxLength(50)]],
      quantity: [0, [Validators.required, Validators.min(0)]],
      purchasePrice: [0, [Validators.required, Validators.min(0)]],
      purchaseDate: ['', Validators.required]
    });
  }

  // Load product data for edit mode.
  loadProduct(id: number): void {
    this.productService.getProductById(id).subscribe({
      next: (response) => {
        const product = response.data;

        this.productForm.patchValue({
          sku: product.sku,
          name: product.name,
          size: product.size,
          color: product.color,
          quantity: product.quantity,
          purchasePrice: product.purchasePrice,
          purchaseDate: product.purchaseDate.substring(0, 10)
        });

        // SKU should not be changed after creation.
        this.productForm.get('sku')?.disable();
      },
      error: (error) => {
        console.error('Product load failed:', error);
        this.errorMessage = 'Failed to load product.';
      }
    });
  }

  // Submit create or update request.
  submitForm(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    if (this.isEditMode) {
      this.updateProduct();
    } else {
      this.createProduct();
    }
  }

  // Create product.
  createProduct(): void {
    const formValue = this.productForm.getRawValue();

    const request: CreateProductRequest = {
      sku: formValue.sku,
      name: formValue.name,
      size: formValue.size || null,
      color: formValue.color || null,
      quantity: Number(formValue.quantity),
      purchasePrice: Number(formValue.purchasePrice),
      purchaseDate: formValue.purchaseDate
    };

    this.productService.createProduct(request).subscribe({
      next: () => {
        // replaceUrl prevents weird back-navigation and refresh issues.
        this.router.navigateByUrl('/products', { replaceUrl: true });
      },
      error: (error) => {
        console.error('Create failed:', error);
        this.errorMessage = error?.error?.message || 'Failed to create product.';
        this.isSubmitting = false;
      }
    });
  }

  // Update product.
  updateProduct(): void {
    const formValue = this.productForm.getRawValue();

    const request: UpdateProductRequest = {
      name: formValue.name,
      size: formValue.size || null,
      color: formValue.color || null,
      quantity: Number(formValue.quantity),
      purchasePrice: Number(formValue.purchasePrice),
      purchaseDate: formValue.purchaseDate
    };

    this.productService.updateProduct(this.productId, request).subscribe({
      next: () => {
        this.router.navigateByUrl('/products', { replaceUrl: true });
      },
      error: (error) => {
        console.error('Update failed:', error);
        this.errorMessage = error?.error?.message || 'Failed to update product.';
        this.isSubmitting = false;
      }
    });
  }

  // Easy access for HTML validation messages.
  get f() {
    return this.productForm.controls;
  }
}
