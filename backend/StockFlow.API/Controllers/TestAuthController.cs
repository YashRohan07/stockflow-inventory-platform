using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockFlow.API.Controllers;

// Utility controller used for testing authentication and authorization behavior.
// Not intended for production use; should be disabled or removed in production environments.
[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
    // Endpoint: GET /api/testauth/protected
    // Purpose: Verify that a valid JWT token is required for access.
    // Access: Any authenticated user.
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "You are authenticated.",
            user = User.Identity?.Name // Extracted from JWT claims
        });
    }

    // Endpoint: GET /api/testauth/admin-only
    // Purpose: Validate Admin-only authorization policy.
    // Access: Only users matching "AdminOnly" policy.
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            message = "You are authorized as Admin."
        });
    }

    // Endpoint: GET /api/testauth/admin-or-member
    // Purpose: Validate role-based access for both Admin and Member.
    // Access: Users matching "AdminOrMember" policy.
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