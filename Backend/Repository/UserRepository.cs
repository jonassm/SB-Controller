using Backend.Repository.Interfaces;
using Core;
using MongoDB.Driver;

namespace Backend.Repository;

public class UserRepository : IUserRepo
{
    private readonly IMongoCollection<User> _userRepository;
    
    public UserRepository()
    {
        string connectionString = Environment.GetEnvironmentVariable("DB_Connection");

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("SB-Controller");
        
        _userRepository = database.GetCollection<User>("User");
    }

    public User Login(string username)
    {
        var filter = Builders<User>.Filter.Eq("username", username);
        return _userRepository.Find(filter).FirstOrDefault();
        //lav et filter, filter efter username.
        //vi tjekker kun for usnermae ved login, dvs. kodeord er null.
    }
    
    public void AddUser(User newUser)
    {
        _userRepository.InsertOne(newUser);
    }
}
