using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs.Auth;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

// Marks this class as an API controller
[ApiController]

// Base route: /api/auth
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // IAuthService is injected from DI
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(response);
    }
}