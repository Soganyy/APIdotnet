namespace Models;

public record Item
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public DateTimeOffset CreateTime { get; set; }
}
