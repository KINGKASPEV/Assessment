using DatabaseApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ProductDbContext>();
        var mongoService = services.GetRequiredService<MongoService>();

        var newEntity = new Product { Name = "Daniel Okafor" };
        context.Products.Add(newEntity);
        context.SaveChanges();

        var newDocument = new BsonDocument { { "name", "Daniel Okafor" } };
        mongoService.InsertDocumentAsync("DeroyalsCollection", newDocument).Wait();

        Console.WriteLine("Data inserted successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(hostContext.Configuration.GetConnectionString("MongoDbConnection")));
            services.AddSingleton<MongoService>();
        });
