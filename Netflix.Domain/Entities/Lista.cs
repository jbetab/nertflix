namespace Netflix.Domain.Entities;

public class Lista
{
    public int Id { get; set; }
    public int ProfileId {get; set;}
    public Profile Profile { get; set; } = null!;
    
    public int? MovieId { get; set; }
    public Movie? Movie { get; set; }
    
    public int? SeriesId { get; set; }
    public Series? Series { get; set; }
}