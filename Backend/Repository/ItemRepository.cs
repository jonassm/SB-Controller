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
        // _ er en expression der gør at alle items vil returner true, dvs. vi får alt ud.
        //det svarer til db.items.find({}) i mongo shell
    }
    public void AddItem(Item item)
    {
        _itemRepository.InsertOne(item);
    }
    
    public void DeleteItem(string name)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.name, name); //find den specfikke item med det navn
        _itemRepository.DeleteOne(filter);
        //slet den
    }
    
}
