namespace Netflix.Domain.Entities;

public class Episode
{
    public int Id { get; set; }
    public int NEpisode { get; set; }
    
    public string VideoUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    
    public int SeasonId { get; set; }
    public Season Season { get; set; } = null!;
}