using Microsoft.AspNetCore.Http;

namespace Netflix.Application.DTOs;

public class CreateProfileDto
{
    public string Name { get; set; } =  string.Empty;
    public IFormFile AvatarFile { get; set; } = null!;
}

public class ProfileResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; } =  string.Empty;
}