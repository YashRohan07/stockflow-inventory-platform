using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using QuestPDF.Infrastructure; 
using StockFlow.API.Extensions;
using StockFlow.API.Middleware;
using StockFlow.Application.Common.Options;
using StockFlow.Infrastructure.DependencyInjection;
using StockFlow.Infrastructure.Persistence;

// QuestPDF license MUST be set before app starts
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Bind JWT configuration to strongly typed JwtOptions.
// This allows JwtOptions to be injected into services using IOptions<JwtOptions>.
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

// Register MVC controllers for API endpoints.
builder.Services.AddControllers();

// Register application-layer services such as AuthService, ProductService, and ReportService.
// Business logic stays in the Application layer, keeping controllers thin.
builder.Services.AddApplicationServices();

// Register infrastructure-layer services such as DbContext, repositories, authentication helpers, and database seeding.
// Infrastructure concerns are kept separate from API and Application layers.
builder.Services.AddInfrastructureServices(builder.Configuration);

// Register Swagger/OpenAPI services for API testing and documentation.
builder.Services.AddEndpointsApiExplorer();

const string bearerScheme = "Bearer";

builder.Services.AddSwaggerGen(options =>
{
    // Adds JWT Bearer authentication support to Swagger UI.
    // This enables testing protected endpoints directly from Swagger.
    options.AddSecurityDefinition(bearerScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste JWT token only. Do not write Bearer manually."
    });

    // Applies the JWT security requirement globally for Swagger requests.
    options.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(bearerScheme, document)] = []
        };
    });
});

// Configure JWT authentication.
// Tokens are validated using issuer, audience, lifetime, and signing key.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey!)),

            // Removes default clock tolerance so token expiry is enforced exactly.
            ClockSkew = TimeSpan.Zero
        };
    });

// Define authorization policies used across protected endpoints.
// Policies centralize role-based access rules instead of duplicating logic in controllers.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("AdminOrMember", policy =>
        policy.RequireRole("Admin", "Member"));
});

// Configure CORS for Angular frontend during local development.
// In production, this should be replaced with the actual frontend domain.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Seed default data required for running and testing the application.
// This is useful for local development and demo environments.
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

// Enable Swagger only in development to avoid exposing API documentation publicly by default.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling should run early so downstream errors return consistent API responses.
app.UseMiddleware<ExceptionMiddleware>();

// Request logging tracks incoming requests, response status codes, and execution time.
app.UseMiddleware<RequestLoggingMiddleware>();

// Apply frontend access policy before authentication/authorization checks.
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

// Authentication must run before Authorization.
// Authentication identifies the user; Authorization checks what the user can access.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();