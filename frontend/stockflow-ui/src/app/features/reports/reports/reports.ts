import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { ReportService } from '../report.service';

import {
  InventoryReportItem,
  InventorySummary
} from '../../../shared/models/report.model';

type ActiveDownloadType = 'full' | 'low-stock' | null;

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
  activeDownloadType: ActiveDownloadType = null;

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

  private resetMessages(): void {
    this.errorMessage = '';
    this.successMessage = '';
  }

  private isInvalidThreshold(): boolean {
    return !this.lowStockThreshold || this.lowStockThreshold <= 0;
  }

  loadSummary(): void {
    this.isLoadingSummary = true;

    this.reportService.getSummary(this.lowStockThreshold)
      .pipe(
        finalize(() => {
          this.isLoadingSummary = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.summary = response.data ?? null;
        },
        error: (error) => {
          console.error('Summary load failed:', error);
          this.errorMessage = 'Failed to load inventory summary.';
        }
      });
  }

  loadFullInventoryReport(): void {
    this.isLoadingReport = true;
    this.resetMessages();

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
          this.successMessage = 'Full inventory report loaded successfully.';
          this.loadSummary();
        },
        error: (error) => {
          console.error('Report load failed:', error);
          this.errorMessage = 'Failed to load inventory report.';
          this.reportItems = [];
        }
      });
  }

  loadLowStockReport(): void {
    if (this.isInvalidThreshold()) {
      this.errorMessage = 'Low stock threshold must be greater than zero.';
      this.successMessage = '';
      return;
    }

    this.isLoadingReport = true;
    this.resetMessages();

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
          this.successMessage = 'Low stock report loaded successfully.';
          this.loadSummary();
        },
        error: (error) => {
          console.error('Low stock report load failed:', error);
          this.errorMessage = 'Failed to load low stock report.';
          this.reportItems = [];
        }
      });
  }

  downloadFullInventoryPdf(): void {
    this.isDownloading = true;
    this.activeDownloadType = 'full';
    this.resetMessages();

    this.reportService.downloadFullInventoryPdf()
      .pipe(
        finalize(() => {
          this.isDownloading = false;
          this.activeDownloadType = null;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (blob) => {
          this.downloadBlob(blob, 'full-inventory-report.pdf');
          this.successMessage = 'Full inventory PDF downloaded successfully.';
        },
        error: (error) => {
          console.error('PDF download failed:', error);
          this.errorMessage = 'Failed to download full inventory PDF.';
        }
      });
  }

  downloadLowStockPdf(): void {
    if (this.isInvalidThreshold()) {
      this.errorMessage = 'Low stock threshold must be greater than zero.';
      this.successMessage = '';
      return;
    }

    this.isDownloading = true;
    this.activeDownloadType = 'low-stock';
    this.resetMessages();

    this.reportService.downloadLowStockPdf(this.lowStockThreshold)
      .pipe(
        finalize(() => {
          this.isDownloading = false;
          this.activeDownloadType = null;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (blob) => {
          this.downloadBlob(blob, 'low-stock-report.pdf');
          this.successMessage = 'Low stock PDF downloaded successfully.';
          this.loadSummary();
        },
        error: (error) => {
          console.error('Low stock PDF download failed:', error);
          this.errorMessage = 'Failed to download low stock PDF.';
        }
      });
  }

  private downloadBlob(blob: Blob, fileName: string): void {
    const url = window.URL.createObjectURL(blob);
    const anchor = document.createElement('a');

    anchor.href = url;
    anchor.download = fileName;
    anchor.style.display = 'none';

    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);

    window.URL.revokeObjectURL(url);
  }
}
