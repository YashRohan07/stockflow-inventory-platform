// Import Component to create Angular component
import { Component, inject, signal } from '@angular/core';

// Import Api service to call backend APIs
import { Api } from '../../../core/services/api';

// Import common API response model
import { ApiResponse } from '../../../shared/models/api-response.model';

// Import health response model
import { HealthStatus } from '../../../shared/models/health-status.model';

// Define Dashboard component
@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  // Inject Api service to call backend
  private readonly api = inject(Api);

  // Store backend health message
  healthMessage = signal('Checking backend connection...');

  // Store backend application name
  applicationName = signal('');

  // Store backend status
  backendStatus = signal('');

  // Constructor runs when component is created
  constructor() {
    // Call backend API: GET /api/health
    this.api.get<ApiResponse<HealthStatus>>('health').subscribe({
      // If API call is successful
      next: (response) => {
        this.healthMessage.set(response.message);
        this.backendStatus.set(response.data?.status ?? '');
        this.applicationName.set(response.data?.application ?? '');
      },

      // If API call fails
      error: () => {
        this.healthMessage.set('Backend connection failed');
        this.backendStatus.set('');
        this.applicationName.set('');
      }
    });
  }
}
