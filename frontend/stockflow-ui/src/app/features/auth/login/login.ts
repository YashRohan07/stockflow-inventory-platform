import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../core/services/auth.service';

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

  // 👁️ Show/Hide password
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = '';

    const request = this.loginForm.value;

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

  // getter for template
  get f() {
    return this.loginForm.controls;
  }
}
