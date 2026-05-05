using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Infrastructure.Authentication;
using StockFlow.Infrastructure.Persistence;
using StockFlow.Infrastructure.Reporting;
using StockFlow.Infrastructure.Repositories;

namespace StockFlow.Infrastructure.DependencyInjection;

// Centralized registration point for Infrastructure layer dependencies.
// Keeps database, repository, authentication, and reporting implementations outside Program.cs.
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register EF Core DbContext with SQL Server.
        // Connection string is loaded from application configuration.
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Register security-related infrastructure services.
        // Application layer depends on abstractions; Infrastructure provides implementations.
        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Register repository implementations for data access.
        // Scoped lifetime aligns with DbContext lifetime per request.
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register PDF generation implementation.
        // Keeps PDF library details isolated inside Infrastructure.
        services.AddScoped<IPdfReportGenerator, PdfReportGenerator>();

        // Register database seeder for local/demo startup data.
        services.AddScoped<DatabaseSeeder>();

        return services;
    }
}