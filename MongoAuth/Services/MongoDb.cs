using MongoDB.Driver;
using MongoAuth.Shared.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace MongoAuth.Services
{
    public class MongoToDoService
    {
        private readonly IMongoCollection<ToDo> _usersCollection;

        public MongoToDoService(IConfiguration configuration)
        {
            var MongoUrl = configuration["MongoDB:TODO:URL"]; 
            var DatabaseName = configuration["MongoDB:TODO:DBNAME"]; 
            var CollectionName = configuration["MongoDB:TODO:COLLECTION"];

            Console.WriteLine("Service Constructor");
            var client = new MongoClient(MongoUrl);
            Console.WriteLine("Connection string works");
            var database = client.GetDatabase(DatabaseName);
            Console.WriteLine("Database get");
            _usersCollection = database.GetCollection<ToDo>(CollectionName);
            Console.WriteLine("Collection get");
        }

        //public async Task<Tasks> GetUserByEmail(string email)
        //{
        //    return await _usersCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
        //}

        public async Task<List<ToDo>> ReadPending()
        {
            var Tasks = await _usersCollection.Find(task => task.Completed == false).ToListAsync();
            return Tasks;
        }
        
        public async Task<List<ToDo>> ReadCompleted()
        {
            var Tasks = await _usersCollection.Find(task => task.Completed == true).ToListAsync();
            return Tasks;
        }

        public async Task CreateToDo(ToDo task)
        {
            await _usersCollection.InsertOneAsync(task);
        }

        public async Task CompleteToDo(ToDo task)
        {
            var id = task.Id;
            var filter = Builders<ToDo>.Filter.Eq(task => task.Id, id);
            var update = Builders<ToDo>.Update.Set(task => task.Completed, true);

            await _usersCollection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveToDo(ToDo task)
        {
            var id = task.Id;
            var filter = Builders<ToDo>.Filter.Eq(task => task.Id, id);
            await _usersCollection.DeleteOneAsync(filter);
        }
    }
}
