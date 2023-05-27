using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories;

public class MongoDbItems : IMemoryItems
{
    private const string databaseName = "catalog";
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> itemsCollection;

    public MongoDbItems()
    {
        var connectionString = ""; 
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        itemsCollection = database.GetCollection<Item>(collectionName);
    }

    public async Task CreateItemAsync(Item item)
    {
        await itemsCollection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsync(Guid id)
    {
        await itemsCollection.DeleteOneAsync(item => item.Id == id);
    }

    public async Task<Item> GetItemAsync(Guid id)
    {
        var item = await itemsCollection.FindAsync(item => item.Id == id);
        return await item.SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        var items = await itemsCollection.FindAsync(new BsonDocument());
        return items.ToList();
    }

    public async Task UpdateItemAsync(Item updatedItem)
    {
        await itemsCollection.FindOneAndUpdateAsync(item => item.Id == updatedItem.Id, 
            Builders<Item>.Update.Set(item => item.Name, updatedItem.Name)
            .Set(item => item.Price, updatedItem.Price));
    }
}