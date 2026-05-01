import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Api } from '../../core/services/api';

import {
  ApiResponse,
  InventoryReportItem,
  InventorySummary
} from '../../shared/models/report.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  constructor(private readonly api: Api) { }

  getSummary(threshold: number): Observable<ApiResponse<InventorySummary>> {
    return this.api.get<ApiResponse<InventorySummary>>(
      `reports/summary?threshold=${threshold}`
    );
  }

  getFullInventoryReport(): Observable<ApiResponse<InventoryReportItem[]>> {
    return this.api.get<ApiResponse<InventoryReportItem[]>>('reports/inventory');
  }

  getLowStockReport(threshold: number): Observable<ApiResponse<InventoryReportItem[]>> {
    return this.api.get<ApiResponse<InventoryReportItem[]>>(
      `reports/low-stock?threshold=${threshold}`
    );
  }

  downloadFullInventoryPdf(): Observable<Blob> {
    return this.api.getBlob('reports/inventory/pdf');
  }

  downloadLowStockPdf(threshold: number): Observable<Blob> {
    return this.api.getBlob(`reports/low-stock/pdf?threshold=${threshold}`);
  }
}
