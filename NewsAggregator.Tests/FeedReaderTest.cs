using System;
using NewsAggregator.Services;
using NewsAggregator.Settings;
using Xunit;

namespace NewsAggregator.Tests
{
    public class FeedReaderTest
    {
        public MongoSettings _testMongoSettings = new MongoSettings 
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "NewsAggregator.Test"
        };

        [Fact]
        public void ReadsBBCNews()
        {
            var reader = new FeedReader();
            Console.WriteLine(reader);
        }
    }
}
