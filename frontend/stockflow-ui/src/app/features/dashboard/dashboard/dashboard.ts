import { Component, inject, signal } from '@angular/core';

import { Api } from '../../../core/services/api';
import { ApiResponse } from '../../../shared/models/api-response.model';
import { HealthStatus } from '../../../shared/models/health-status.model';

// Dashboard component used to verify backend connectivity.
// Displays API health status returned from /api/health.
@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class Dashboard {
  private readonly api = inject(Api);

  healthMessage = signal('Checking backend connection...');
  applicationName = signal('');
  backendStatus = signal('');

  constructor() {
    // Load backend health status when dashboard is initialized.
    this.api.get<ApiResponse<HealthStatus>>('health').subscribe({
      next: (response) => {
        this.healthMessage.set(response.message);
        this.backendStatus.set(response.data?.status ?? '');
        this.applicationName.set(response.data?.application ?? '');
      },
      error: () => {
        this.healthMessage.set('Backend connection failed');
        this.backendStatus.set('');
        this.applicationName.set('');
      }
    });
  }
}
