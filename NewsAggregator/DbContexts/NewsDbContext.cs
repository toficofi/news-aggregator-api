using Blueshift.EntityFrameworkCore.MongoDB.Annotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NewsAggregator.Models;

namespace NewsAggregator.DbContexts 
{
    public class NewsDbContext : DbContext
    {
        public DbSet<NewsItem> News { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options): base(options)
        {

        }
        
        public NewsDbContext(string connectionString): base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            var mongoClient = new MongoClient(settings);
            return MongoDbContextOptionsBuilderExtensions.UseMongoDb(new DbContextOptionsBuilder(), mongoClient).Options;
        }
    }
}