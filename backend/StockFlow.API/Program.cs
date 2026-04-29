using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StockFlow.API.Extensions;
using StockFlow.API.Middleware;
using StockFlow.Infrastructure.DependencyInjection;
using StockFlow.Infrastructure.Persistence;
using StockFlow.Application.Common.Options;

var builder = WebApplication.CreateBuilder(args);

// Bind JWT settings from appsettings.json
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

// Add controllers
builder.Services.AddControllers();

// Register Application services
// Example: AuthService, ProductService
builder.Services.AddApplicationServices();

// Register Infrastructure services
// Example: DbContext, repositories, password hasher, JWT generator, seeder
builder.Services.AddInfrastructureServices(builder.Configuration);

// Swagger with JWT Bearer support
builder.Services.AddEndpointsApiExplorer();

const string bearerScheme = "Bearer";

builder.Services.AddSwaggerGen(options =>
{
    // Add Authorize button in Swagger
    options.AddSecurityDefinition(bearerScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste JWT token only. Do not write Bearer manually."
    });

    // Attach Authorization header to Swagger requests
    options.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(bearerScheme, document)] = []
        };
    });
});

// JWT Authentication
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

            ClockSkew = TimeSpan.Zero
        };
    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("AdminOrMember", policy =>
        policy.RequireRole("Admin", "Member"));
});

// CORS for Angular frontend
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

// Seed default users
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();