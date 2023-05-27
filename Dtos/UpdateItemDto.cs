namespace Dtos;

public record UpdateItemDto
{
    public string? Name { get; init; }
    public double Price { get; init;}
}