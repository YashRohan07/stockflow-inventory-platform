using StockFlow.API.Middleware;
using StockFlow.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add controller support
// Controllers will handle HTTP requests (GET, POST, PUT, DELETE)
builder.Services.AddControllers();

// Add Swagger services
// This provides API documentation and testing UI in browser
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Infrastructure services
// This includes AppDbContext and SQL Server connection
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add CORS policy
// This allows Angular frontend to call this backend API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Angular app URL
            .AllowAnyHeader()                     // Allow request headers
            .AllowAnyMethod();                    // Allow GET, POST, PUT, DELETE
    });
});

var app = builder.Build();

// Enable Swagger only in development environment
// We usually disable Swagger in production for security reasons
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global error handling middleware
// This catches all unhandled exceptions and returns clean JSON response
app.UseMiddleware<ExceptionMiddleware>();

// Request logging middleware
// This logs every request (method, path, status code, execution time)
app.UseMiddleware<RequestLoggingMiddleware>();

// Enable CORS middleware
// Must be before MapControllers so controllers can accept frontend requests
app.UseCors("AllowFrontend");

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Map controller routes
// Example: /api/health → HealthController
app.MapControllers();

// Start the application
app.Run();