// Needed for working with Entity Framework Core (database)
using Microsoft.EntityFrameworkCore;

// Used to read configuration (like connection string from appsettings.json)
using Microsoft.Extensions.Configuration;

// Used for Dependency Injection (registering services)
using Microsoft.Extensions.DependencyInjection;

// Importing interfaces from Application layer
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;

// Importing implementations from Infrastructure layer
using StockFlow.Infrastructure.Authentication;
using StockFlow.Infrastructure.Persistence;
using StockFlow.Infrastructure.Reporting;
using StockFlow.Infrastructure.Repositories;

namespace StockFlow.Infrastructure.DependencyInjection;

// Static class for registering all Infrastructure services
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register AppDbContext (Database Context)
        // This configures EF Core to use SQL Server with connection string
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Register Password Hasher Service
        // Handles hashing and verifying passwords securely
        services.AddScoped<IPasswordHasher, PasswordHasherService>();

        // Register JWT Token Generator
        // Generates JWT tokens after successful login
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Register User Repository
        // Application layer will use IUserRepository
        // Infrastructure provides the actual implementation
        services.AddScoped<IUserRepository, UserRepository>();

        // Register Product Repository
        // Application layer will use IProductRepository
        // Infrastructure provides the actual database implementation
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register PDF report generator
        // Application depends on IPdfReportGenerator
        // Infrastructure provides the actual QuestPDF implementation
        services.AddScoped<IPdfReportGenerator, PdfReportGenerator>();

        // Register Database Seeder
        // Seeds default Admin and Member users on application startup
        services.AddScoped<DatabaseSeeder>();

        // Return services so we can chain registrations
        return services;
    }
}