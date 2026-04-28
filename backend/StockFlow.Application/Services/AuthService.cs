using StockFlow.Application.DTOs.Auth;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Application.Common.Exceptions;

namespace StockFlow.Application.Services;

// This service handles authentication (login) business logic
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        // Normalize email (avoid case issues)
        var email = request.Email.Trim().ToLower();

        // 🔹 Step 1: Find user by email
        var user = await _userRepository.GetByEmailAsync(email);

        // 🔹 Step 2: If user not found → throw custom exception
        if (user is null)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        // 🔹 Step 3: Verify password (plain vs hashed)
        var isPasswordValid = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        // 🔹 Step 4: Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // 🔹 Step 5: Return response DTO (safe data only)
        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = _jwtTokenGenerator.GetTokenExpiryTime(),
            User = new LoggedInUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };
    }
}