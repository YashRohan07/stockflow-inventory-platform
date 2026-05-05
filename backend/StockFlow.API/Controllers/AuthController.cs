using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs.Auth;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Handles all authentication-related business logic (login, token generation)
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        // Dependency Injection ensures loose coupling and testability
        _authService = authService;
    }

    // Endpoint: POST /api/auth/login
    // Purpose: Authenticate user credentials and return JWT token + user info
    // Note: Validation (if configured via FluentValidation) will run before reaching this method
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        // Delegates authentication logic to Application layer (keeps controller thin)
        var response = await _authService.LoginAsync(request);

        // Always returns 200 OK with standardized response structure
        // (Errors should be handled via global exception middleware)
        return Ok(response);
    }
}