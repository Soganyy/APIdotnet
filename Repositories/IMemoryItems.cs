using Models;

namespace Repositories;

public interface IMemoryItems
{
    Task<IEnumerable<Item>> GetItemsAsync();
    Task<Item> GetItemAsync(Guid id);
    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(Item updatedItem);
    Task DeleteItemAsync(Guid id);
}