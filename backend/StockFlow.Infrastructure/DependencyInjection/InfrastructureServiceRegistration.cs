// Needed for working with Entity Framework Core (database)
using Microsoft.EntityFrameworkCore;

// Used to read configuration (like connection string from appsettings.json)
using Microsoft.Extensions.Configuration;

// Used for Dependency Injection (registering services)
using Microsoft.Extensions.DependencyInjection;

// Importing AppDbContext (our main database class)
using StockFlow.Infrastructure.Persistence;

// Namespace for dependency injection setup in Infrastructure layer
namespace StockFlow.Infrastructure.DependencyInjection;

// Static class because we are creating extension methods
public static class InfrastructureServiceRegistration
{
    // This method is used to register Infrastructure services
    // It extends IServiceCollection so we can call it in Program.cs
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register AppDbContext in Dependency Injection container
        // This tells ASP.NET Core how to create AppDbContext when needed
        services.AddDbContext<AppDbContext>(options =>

            // Configure EF Core to use SQL Server
            options.UseSqlServer(

                // Read connection string from appsettings.json
                configuration.GetConnectionString("DefaultConnection")));

        // Return services so we can chain more registrations if needed
        return services;
    }
}