using Netflix.Application.DTOs;
using Netflix.Application.Interfaces;
using Netflix.Domain.Entities;
using Netflix.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Netflix.Infrastructure.Services;

public class AuthServices : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IConfiguration _config;

    public AuthServices(IRepository<User> userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<UserAuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        
        var user = new User 
        { 
            Email = registerDto.Email, 
            Password = hashedPassword
            // Si agregaste Name a tu entidad User, asígnalo aquí: Name = registerDto.Name
        };

        await _userRepo.AddAsync(user);
        await _userRepo.SaveAsync();

        return CreateUserAuthResponse(user);
    }

    public async Task<UserAuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var users = await _userRepo.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            throw new Exception("Credenciales inválidas");

        return CreateUserAuthResponse(user);
    }

    public async Task<UserAuthResponseDto> RefreshTokenAsync(RefreshDto refreshDto)
    {
        // Lógica simplificada: En un entorno real validarías el RefreshToken en DB
        // Por ahora, lanzamos una excepción para recordarnos implementarlo luego
        throw new NotImplementedException("La validación de Refresh Token requiere lógica de BD adicional.");
    }

    private UserAuthResponseDto CreateUserAuthResponse(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email) 
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new UserAuthResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Token = tokenHandler.WriteToken(token),
            RefreshToken = Guid.NewGuid().ToString() // Token temporal
        };
    }
}