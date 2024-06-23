using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DatabaseApp
{
    public class MongoService
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoService(IMongoClient mongoClient, IConfiguration configuration)
        {
            _mongoClient = mongoClient;
            var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value;
            _database = _mongoClient.GetDatabase(databaseName);
        }

        public async Task InsertDocumentAsync<T>(string collectionName, T document)
        {
            var collection = _database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }
    }
}
