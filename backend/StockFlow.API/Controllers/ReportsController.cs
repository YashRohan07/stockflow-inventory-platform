using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

// Handles inventory reporting endpoints for summary, full inventory, low-stock, date-range, and PDF reports.
// Controller delegates report calculation and PDF generation to the Application layer.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    // Application service responsible for report data preparation and PDF generation.
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        // Dependency Injection keeps reporting logic separate from the API layer.
        _reportService = reportService;
    }

    // Endpoint: GET /api/reports/summary
    // Purpose: Return inventory summary data such as stock totals and low-stock overview.
    // Access: Admin and Member can view summary-level inventory information.
    [HttpGet("summary")]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetInventorySummary([FromQuery] int threshold = 20)
    {
        var summary = await _reportService.GetInventorySummaryAsync(threshold);

        return Ok(new
        {
            success = true,
            message = "Inventory summary retrieved successfully.",
            data = summary
        });
    }

    // Endpoint: GET /api/reports/inventory
    // Purpose: Return the full inventory report as structured JSON data.
    // Access: Admin and Member can view inventory report data.
    [HttpGet("inventory")]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetFullInventoryReport()
    {
        var report = await _reportService.GetFullInventoryReportAsync();

        return Ok(new
        {
            success = true,
            message = "Full inventory report retrieved successfully.",
            data = report
        });
    }

    // Endpoint: GET /api/reports/low-stock
    // Purpose: Return products where stock quantity is below or equal to the selected threshold.
    // Access: Admin and Member can monitor low-stock items.
    [HttpGet("low-stock")]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetLowStockReport([FromQuery] int threshold = 20)
    {
        var report = await _reportService.GetLowStockReportAsync(threshold);

        return Ok(new
        {
            success = true,
            message = "Low stock report retrieved successfully.",
            data = report
        });
    }

    // Endpoint: GET /api/reports/date-range
    // Purpose: Return inventory records filtered by purchase date range.
    // Access: Admin and Member can analyze inventory purchases within a selected period.
    // Note: Date validation and business rules are handled inside the service layer.
    [HttpGet("date-range")]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetDateRangeReport(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        var report = await _reportService.GetDateRangeReportAsync(from, to);

        return Ok(new
        {
            success = true,
            message = "Date range report retrieved successfully.",
            data = report
        });
    }

    // Endpoint: GET /api/reports/inventory/pdf
    // Purpose: Generate and download the full inventory report as a PDF file.
    // Access: Only Admin can export official report files.
    [HttpGet("inventory/pdf")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DownloadFullInventoryPdf()
    {
        var pdfBytes = await _reportService.GenerateFullInventoryPdfAsync();

        return File(
            pdfBytes,
            "application/pdf",
            "full-inventory-report.pdf");
    }

    // Endpoint: GET /api/reports/low-stock/pdf
    // Purpose: Generate and download the low-stock report as a PDF file.
    // Access: Only Admin can export official low-stock report files.
    [HttpGet("low-stock/pdf")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DownloadLowStockPdf([FromQuery] int threshold = 20)
    {
        var pdfBytes = await _reportService.GenerateLowStockPdfAsync(threshold);

        return File(
            pdfBytes,
            "application/pdf",
            "low-stock-report.pdf");
    }
}