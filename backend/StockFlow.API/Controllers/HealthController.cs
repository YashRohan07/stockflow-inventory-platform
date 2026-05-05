using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Common.Models;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    // Endpoint: GET /api/health
    // Purpose: Check API availability and basic system status
    // Note: Can be extended later to include DB, cache, external service checks
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "API is healthy",
            Data = new
            {
                status = "Healthy",
                application = "StockFlow API",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                timestamp = DateTime.UtcNow
            },
            Errors = null
        });
    }
}