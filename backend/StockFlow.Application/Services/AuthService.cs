using StockFlow.Application.Common.Exceptions;
using StockFlow.Application.DTOs.Auth;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.Application.Services;

// Handles authentication business logic.
// Coordinates user lookup, password verification, JWT generation, and safe response creation.
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
        // Normalize email to avoid case-sensitivity issues during login.
        var email = request.Email.Trim().ToLower();

        var user = await _userRepository.GetByEmailAsync(email);

        // Return the same error message for missing user and invalid password
        // to avoid revealing whether an email exists in the system.
        if (user is null)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        // Verify plain-text input against the stored password hash.
        // Password hashing details are hidden behind IPasswordHasher.
        var isPasswordValid = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        // Generate a signed JWT after credentials are successfully verified.
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Return only safe user information required by the frontend.
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