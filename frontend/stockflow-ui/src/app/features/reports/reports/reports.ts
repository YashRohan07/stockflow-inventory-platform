import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { ReportService } from '../report.service';

import {
  InventoryReportItem,
  InventorySummary
} from '../../../shared/models/report.model';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports.html',
  styleUrls: ['./reports.scss']
})
export class Reports implements OnInit {
  summary: InventorySummary | null = null;

  reportItems: InventoryReportItem[] = [];

  lowStockThreshold = 20;

  isLoadingSummary = false;
  isLoadingReport = false;
  isDownloading = false;

  errorMessage = '';
  successMessage = '';

  constructor(
    private readonly reportService: ReportService,
    private readonly cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadSummary();
    this.loadFullInventoryReport();
  }

  loadSummary(): void {
    this.isLoadingSummary = true;
    this.errorMessage = '';

    this.reportService.getSummary(this.lowStockThreshold)
      .pipe(
        finalize(() => {
          this.isLoadingSummary = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.summary = response.data;
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('Summary load failed:', error);
          this.errorMessage = 'Failed to load inventory summary.';
          this.cdr.detectChanges();
        }
      });
  }

  loadFullInventoryReport(): void {
    this.isLoadingReport = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.reportService.getFullInventoryReport()
      .pipe(
        finalize(() => {
          this.isLoadingReport = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.reportItems = response.data ?? [];
          this.successMessage = 'Full inventory report loaded.';
          this.loadSummary();
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('Report load failed:', error);
          this.errorMessage = 'Failed to load inventory report.';
          this.reportItems = [];
          this.cdr.detectChanges();
        }
      });
  }

  loadLowStockReport(): void {
    if (this.lowStockThreshold <= 0) {
      this.errorMessage = 'Low stock threshold must be greater than zero.';
      return;
    }

    this.isLoadingReport = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.reportService.getLowStockReport(this.lowStockThreshold)
      .pipe(
        finalize(() => {
          this.isLoadingReport = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.reportItems = response.data ?? [];
          this.successMessage = 'Low stock report loaded.';
          this.loadSummary();
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('Low stock report load failed:', error);
          this.errorMessage = 'Failed to load low stock report.';
          this.reportItems = [];
          this.cdr.detectChanges();
        }
      });
  }

  downloadFullInventoryPdf(): void {
    this.isDownloading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.reportService.downloadFullInventoryPdf()
      .pipe(
        finalize(() => {
          this.isDownloading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (blob) => {
          this.downloadBlob(blob, 'full-inventory-report.pdf');
          this.successMessage = 'Full inventory PDF downloaded.';
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('PDF download failed:', error);
          this.errorMessage = 'Failed to download PDF.';
          this.cdr.detectChanges();
        }
      });
  }

  downloadLowStockPdf(): void {
    if (this.lowStockThreshold <= 0) {
      this.errorMessage = 'Low stock threshold must be greater than zero.';
      return;
    }

    this.isDownloading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.reportService.downloadLowStockPdf(this.lowStockThreshold)
      .pipe(
        finalize(() => {
          this.isDownloading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (blob) => {
          this.downloadBlob(blob, 'low-stock-report.pdf');
          this.successMessage = 'Low stock PDF downloaded.';
          this.loadSummary();
          this.cdr.detectChanges();
        },
        error: (error) => {
          console.error('Low stock PDF download failed:', error);
          this.errorMessage = 'Failed to download low stock PDF.';
          this.cdr.detectChanges();
        }
      });
  }

  private downloadBlob(blob: Blob, fileName: string): void {
    const url = window.URL.createObjectURL(blob);
    const anchor = document.createElement('a');

    anchor.href = url;
    anchor.download = fileName;
    anchor.click();

    window.URL.revokeObjectURL(url);
  }
}
