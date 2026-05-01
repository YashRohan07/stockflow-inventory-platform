using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

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