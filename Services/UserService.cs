using MongoDB.Bson;
using MongoDB.Driver;
using Ecommerce.Models.Entities;

namespace Ecommerce.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("UserDb"));
            var database = client.GetDatabase("UserDb");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAsync() =>
            await _users.Find(user => true).ToListAsync();

        public async Task<User?> GetAsync(string id)
        {
            var objectId = ObjectId.Parse(id); // Converti la stringa in ObjectId
            return await _users.Find<User>(user => user.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(string id, User updateUser)
        {
            var objectId = ObjectId.Parse(id);
            await _users.ReplaceOneAsync(user => user.Id == objectId, updateUser);
        }

        public async Task RemoveAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _users.DeleteOneAsync(user => user.Id == objectId);
        }

        public async Task<User> RegisterAsync(User user, bool isAdmin = false)
        {
            if (isAdmin)
            {
                user.Roles = new List<string> { "Admin" };
            }
            else
            {
                user.Roles = new List<string> { "User" };
            }

            await _users.InsertOneAsync(user);
            return user;
        }
    }
}
