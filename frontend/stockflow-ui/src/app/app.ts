import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  constructor(
    // Public so template can access authService.isLoggedIn()
    public authService: AuthService,

    // Router used for redirect after logout
    private router: Router
  ) { }

  // Logout user and redirect to login page
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
