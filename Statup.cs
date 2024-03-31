// In your Startup.cs file

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AuthApp.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add MongoDB configuration
        services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

        // Add MongoDB client
        services.AddSingleton<IMongoClient, MongoClient>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        // Add MongoDB database
        services.AddScoped<IMongoDatabase>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        // Register UserRepository
        services.AddScoped<UserRepository>();

        // Add other services and controllers here
    }

    // Other methods in Startup class
}
