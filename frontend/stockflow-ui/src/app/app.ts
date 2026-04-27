// Import Component from Angular core
import { Component } from '@angular/core';

// Import RouterLink for navigation links
// Import RouterOutlet to display routed pages
import { RouterLink, RouterOutlet } from '@angular/router';

// Root component of the Angular application
@Component({
  selector: 'app-root',              // HTML tag used to load this component
  standalone: true,                  // Modern Angular standalone component
  imports: [RouterOutlet, RouterLink], // Enable routing and router links
  templateUrl: './app.html',         // HTML file for this component
  styleUrl: './app.scss'             // SCSS file for this component
})
export class App {
  // No logic needed here yet
}
