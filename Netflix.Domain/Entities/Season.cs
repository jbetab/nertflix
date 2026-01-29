namespace Netflix.Domain.Entities;

public class Season
{
    public int Id { get; set; }
    public int NSeason { get; set; }
    
    public int SeriesId { get; set; }
    public Series Series { get; set; } = null!;

    public List<Episode> Episodes { get; set; } = new();
}