using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace mongodb_dotnet_example.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _users;

        public UsersService(IUsersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() => _users.Find(user => true).ToList();

        public User Get(string id) => _users.Find(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User updatedUser) => _users.ReplaceOne(user => user.Id == id, updatedUser);

        public void Delete(User userForDeletion) => _users.DeleteOne(user => user.Id == userForDeletion.Id);

        public void Delete(string id) => _users.DeleteOne(user => user.Id == id);
    }
}
