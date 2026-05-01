using StockFlow.Application.Interfaces.Services;
using StockFlow.Application.Services;

namespace StockFlow.API.Extensions;

// This class registers application services.
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AuthService.
        services.AddScoped<IAuthService, AuthService>();

        // Register ProductService.
        services.AddScoped<IProductService, ProductService>();

        // Register ReportService.
        services.AddScoped<IReportService, ReportService>();

        return services;
    }
}