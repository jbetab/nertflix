using Netflix.Application.DTOs;

namespace Netflix.Application.Interfaces;

public interface IAuthService
{
    Task<UserAuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<UserAuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<UserAuthResponseDto> RefreshTokenAsync(RefreshDto refreshDto);
}