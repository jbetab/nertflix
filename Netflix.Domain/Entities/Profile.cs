namespace Netflix.Domain.Entities;

public class Profile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Avatar {get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public List<Lista> MyList { get; set; } = new();
}