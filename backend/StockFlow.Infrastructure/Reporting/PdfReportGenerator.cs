using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using StockFlow.Application.DTOs.Reports;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.Infrastructure.Reporting;

public class PdfReportGenerator : IPdfReportGenerator
{
    public byte[] GenerateInventoryReportPdf(
        string title,
        List<InventoryReportItemDto> items,
        InventorySummaryDto summary)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(32);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .PaddingBottom(16)
                    .Column(column =>
                    {
                        column.Item().Text("StockFlow")
                            .FontSize(22)
                            .Bold();

                        column.Item().Text(title)
                            .FontSize(16)
                            .SemiBold();

                        column.Item().Text($"Generated at: {DateTime.Now:yyyy-MM-dd HH:mm}")
                            .FontSize(9)
                            .FontColor(Colors.Grey.Darken1);
                    });

                page.Content()
                    .Column(column =>
                    {
                        column.Spacing(14);

                        column.Item().Text("Summary")
                            .FontSize(13)
                            .Bold();

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCell).Text("Products");
                                header.Cell().Element(HeaderCell).Text("Quantity");
                                header.Cell().Element(HeaderCell).Text("Avg Price");
                                header.Cell().Element(HeaderCell).Text("Total Value");
                                header.Cell().Element(HeaderCell).Text("Low Stock");
                            });

                            table.Cell().Element(BodyCell).Text(summary.TotalProducts.ToString());
                            table.Cell().Element(BodyCell).Text(summary.TotalQuantity.ToString());
                            table.Cell().Element(BodyCell).Text(FormatMoney(summary.AveragePrice));
                            table.Cell().Element(BodyCell).Text(FormatMoney(summary.TotalInventoryValue));
                            table.Cell().Element(BodyCell).Text(summary.LowStockProducts.ToString());
                        });

                        column.Item().Text("Report Items")
                            .FontSize(13)
                            .Bold();

                        if (!items.Any())
                        {
                            column.Item()
                                .PaddingTop(8)
                                .Text("No report data found.")
                                .FontColor(Colors.Grey.Darken1);
                        }
                        else
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(1.8f);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(HeaderCell).Text("SKU");
                                    header.Cell().Element(HeaderCell).Text("Name");
                                    header.Cell().Element(HeaderCell).Text("Size");
                                    header.Cell().Element(HeaderCell).Text("Color");
                                    header.Cell().Element(HeaderCell).Text("Qty");
                                    header.Cell().Element(HeaderCell).Text("Price");
                                    header.Cell().Element(HeaderCell).Text("Value");
                                });

                                foreach (var item in items)
                                {
                                    table.Cell().Element(BodyCell).Text(item.SKU);
                                    table.Cell().Element(BodyCell).Text(item.Name);
                                    table.Cell().Element(BodyCell).Text(item.Size ?? "-");
                                    table.Cell().Element(BodyCell).Text(item.Color ?? "-");
                                    table.Cell().Element(BodyCell).Text(item.Quantity.ToString());
                                    table.Cell().Element(BodyCell).Text(FormatMoney(item.PurchasePrice));
                                    table.Cell().Element(BodyCell).Text(FormatMoney(item.TotalValue));
                                }
                            });
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                        text.Span(" | Generated by StockFlow System");
                    });
            });
        }).GeneratePdf();
    }

    private static string FormatMoney(decimal value)
    {
        return $"{value:0.00}";
    }

    private static IContainer HeaderCell(IContainer container)
    {
        return container
            .Background(Colors.Grey.Lighten3)
            .Border(1)
            .BorderColor(Colors.Grey.Lighten1)
            .Padding(5);
    }

    private static IContainer BodyCell(IContainer container)
    {
        return container
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(5);
    }
}