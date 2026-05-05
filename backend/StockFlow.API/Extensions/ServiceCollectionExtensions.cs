using StockFlow.Application.Interfaces.Services;
using StockFlow.Application.Services;

namespace StockFlow.API.Extensions;

// Centralized registration point for application-layer services.
// Keeps Program.cs clean and ensures a single place to manage service dependencies.
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register authentication-related business logic (JWT generation, login validation, etc.)
        services.AddScoped<IAuthService, AuthService>();

        // Register product and inventory business logic (CRUD, search, filtering, pagination)
        services.AddScoped<IProductService, ProductService>();

        // Register reporting logic (summary, low-stock, PDF generation, analytics)
        services.AddScoped<IReportService, ReportService>();

        // Scoped lifetime ensures one instance per request (recommended for business services)
        return services;
    }
}