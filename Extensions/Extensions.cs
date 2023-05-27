using Dtos;
using Models;

namespace Extensions;

public static class Extensions 
{
    public static ItemDto AsDto(Item item) 
    {
        return new ItemDto 
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreateTime = item.CreateTime
        };
    }
}