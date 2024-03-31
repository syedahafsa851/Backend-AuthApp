using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthApp.Services
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase? _users;
        public MongoDBContext(IOptions<Options.DatabaseSettings> dataBaseSettings)
        {
            Guard.Against.Null(dataBaseSettings.Value);
            var client = new MongoClient(dataBaseSettings.Value.ConnectionString);
            _users = client.GetDatabase(dataBaseSettings.Value.DatabaseName);

        }
        //public IMongoCollection<UsersDbModel> Users => _database!.GetCollection<UsersDbModel>("users");
    }
}