using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Common.Models;

namespace StockFlow.API.Controllers;

// Marks this class as an API controller
[ApiController]

// Base route: api/health
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    // GET: api/health
    // This endpoint checks if API is running properly
    [HttpGet]
    public IActionResult GetHealth()
    {
        // Return a standard API response
        return Ok(new ApiResponse<object>
        {
            Success = true,                     // Request Success
            Message = "API is healthy",         // Simple message
            Data = new
            {
                status = "Healthy",             // API status
                application = "StockFlow API",  // Application name
                timestamp = DateTime.UtcNow     // Current time (UTC)
            },
            Errors = null                       // No errors
        });
    }
}