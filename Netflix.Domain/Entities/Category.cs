namespace Netflix.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Genre {get; set;} = string.Empty;
    public int AgeFilter {get; set;}

    public List<Movie> Movies { get; set; } = new();
    public List<Series> Series { get; set; } = new();
}