namespace Netflix.Domain.Entities;

public class Series
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public List<Season> Seasons { get; set; } = new();
}