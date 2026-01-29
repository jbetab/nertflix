namespace Netflix.Application.DTOs;

public class RegisterDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserAuthResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}