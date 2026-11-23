using Backend.Repository.Interfaces;
using Core;
using MongoDB.Driver;

namespace Backend.Repository;

public class ItemRepository : IItemRepo
{
    private readonly IMongoCollection<Item> _itemRepository;
    
    public ItemRepository()
    {
        string connectionString = Environment.GetEnvironmentVariable("DB_Connection");

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("SB-Controller");
        
        _itemRepository = database.GetCollection<Item>("Item");
        
    }

    public List<Item> GetAllItems()
    {
        return _itemRepository.Find(_ => true).ToList();
    }
    public void AddItem(Item item)
    {
        _itemRepository.InsertOne(item);
    }
    
    public void DeleteItem(string name)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.name, name);
        _itemRepository.DeleteOne(filter);
        //slet den
    }
    
}
