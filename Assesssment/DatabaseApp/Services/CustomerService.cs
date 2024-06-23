using DatabaseApp.Context;
using DatabaseApp.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace DatabaseApp.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerService()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MongoDbConnection");
            string databaseName = configuration["MongoSettings:DatabaseName"];
            var mongoDbContext = new MongoDbContext(connectionString, databaseName);

            _customerCollection = mongoDbContext.Customers;
        }

        public void AddCustomer(Customer customer)
        {
            _customerCollection.InsertOne(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerCollection.Find(Builders<Customer>.Filter.Empty).ToList();
        }
    }
}
