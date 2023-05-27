using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers;

[ApiController]
[Route("[controller]")]

public class ItemController : ControllerBase
{
    private readonly IMemoryItems items;

    public ItemController(IMemoryItems items)
    {
        this.items = items;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = (await this.items.GetItemsAsync()).Select(item => new ItemDto{
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreateTime = item.CreateTime
        });
        return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItemAsync(Guid id)
    {
        var item = await this.items.GetItemAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreateTime = DateTimeOffset.UtcNow
        };

        await this.items.CreateItemAsync(item);

        return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item);
    }  

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
        var existingItem = await this.items.GetItemAsync(id);
        if (existingItem is null)
        {
            return NotFound();
        }

        Item updatedItem = existingItem with
        {
            Name = itemDto.Name,
            Price = itemDto.Price
        };

        await this.items.UpdateItemAsync(updatedItem);

        return NoContent();
    } 

    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemDto>> DeleteItemAsync(Guid id)
    {
        var existingItem = await this.items.GetItemAsync(id);
        if (existingItem is null)
        {
            return NotFound();
        }

        await this.items.DeleteItemAsync(id);

        return NoContent();
    }
}