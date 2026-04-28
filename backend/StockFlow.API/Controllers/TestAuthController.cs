using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
    // Anyone with a valid JWT token can access this
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "You are authenticated.",
            user = User.Identity?.Name
        });
    }

    // Only Admin role can access this
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            message = "You are authorized as Admin."
        });
    }

    // Admin and Member both can access this
    [Authorize(Policy = "AdminOrMember")]
    [HttpGet("admin-or-member")]
    public IActionResult AdminOrMember()
    {
        return Ok(new
        {
            message = "You are authorized as Admin or Member."
        });
    }
}