import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Api } from '../../core/services/api';
import { ApiResponse } from '../../shared/models/api-response.model';

import {
  InventoryReportItem,
  InventorySummary
} from '../../shared/models/report.model';

// Handles report-related API calls.
// Keeps report components focused on UI logic instead of endpoint details.
@Injectable({
  providedIn: 'root'
})
export class ReportService {
  constructor(private readonly api: Api) { }

  // Retrieves inventory summary metrics for dashboard/report cards.
  getSummary(threshold: number): Observable<ApiResponse<InventorySummary>> {
    return this.api.get<ApiResponse<InventorySummary>>(
      `reports/summary?threshold=${threshold}`
    );
  }

  // Retrieves full inventory report data as JSON.
  getFullInventoryReport(): Observable<ApiResponse<InventoryReportItem[]>> {
    return this.api.get<ApiResponse<InventoryReportItem[]>>('reports/inventory');
  }

  // Retrieves low-stock report data based on the selected threshold.
  getLowStockReport(threshold: number): Observable<ApiResponse<InventoryReportItem[]>> {
    return this.api.get<ApiResponse<InventoryReportItem[]>>(
      `reports/low-stock?threshold=${threshold}`
    );
  }

  // Downloads the full inventory report as a PDF file.
  downloadFullInventoryPdf(): Observable<Blob> {
    return this.api.getBlob('reports/inventory/pdf');
  }

  // Downloads the low-stock report as a PDF file.
  downloadLowStockPdf(threshold: number): Observable<Blob> {
    return this.api.getBlob(`reports/low-stock/pdf?threshold=${threshold}`);
  }
}
