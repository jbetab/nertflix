namespace Netflix.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Duration { get; set; } 
    public string Autor { get; set; } = string.Empty;
    
    public string videoUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}