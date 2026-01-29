using back.Domain.Enums;

namespace Netflix.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime RegisterDate { get; set; }
    public bool Active { get; set; }
    public int Age { get; set; }
    public UserRole Role  { get; set; }

    public List<Profile> Profiles { get; set; } = new();
}