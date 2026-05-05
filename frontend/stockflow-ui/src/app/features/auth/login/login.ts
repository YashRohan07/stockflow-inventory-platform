import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../core/services/auth.service';

// Handles user login form, validation, password visibility, and post-login navigation.
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrls: ['./login.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage = '';
  isSubmitting = false;

  showPassword = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    // Reactive form validation mirrors backend login input requirements.
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  // Toggles password field visibility for better login UX.
  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  // Validates credentials and redirects authenticated users to product inventory.
  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const request = {
      email: this.loginForm.value.email.trim().toLowerCase(),
      password: this.loginForm.value.password
    };

    this.authService.login(request).subscribe({
      next: () => {
        this.router.navigate(['/products']);
      },
      error: () => {
        this.errorMessage = 'Invalid email or password';
        this.isSubmitting = false;
      }
    });
  }

  // Shortcut for template validation access.
  get f() {
    return this.loginForm.controls;
  }
}
