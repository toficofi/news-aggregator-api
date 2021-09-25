using System;
using System.Linq;
using NewsAggregator.Models;
using NewsAggregator.Services;
using NewsAggregator.Settings;
using Xunit;

namespace NewsAggregator.Tests
{
    public class FeedReaderTest: IClassFixture<LoggingFixture>
    {
        private readonly LoggingFixture _loggingFixture;

        public FeedReaderTest(LoggingFixture loggingFixture) 
        {
            _loggingFixture = loggingFixture;
        }

        public MongoSettings _testMongoSettings = new MongoSettings 
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "NewsAggregator.Test"
        };

        private static void TestNewsItem(NewsItem newsItem) 
        {
            Assert.NotNull(newsItem);
            Assert.NotNull(newsItem.Source);
            Assert.False(string.IsNullOrEmpty(newsItem.Id));
            Assert.False(string.IsNullOrEmpty(newsItem.Summary));
            Assert.False(string.IsNullOrEmpty(newsItem.Headline));
            Assert.False(string.IsNullOrEmpty(newsItem.Url));
            Assert.False(string.IsNullOrEmpty(newsItem.Source.Copyright));
            Assert.False(string.IsNullOrEmpty(newsItem.Source.ThumbnailUrl));
            Assert.False(string.IsNullOrEmpty(newsItem.Source.Title));
        }

        private static void TestNewsReader(INewsReader reader) 
        {
            Assert.True(reader.TryReadNewsItems(out var newsItems));

            Assert.NotNull(newsItems);
            Assert.NotEmpty(newsItems);
            Assert.True(newsItems.Count > 5); // We'd probably expect at least 5 news items from BBC

            foreach (var kvp in newsItems) 
            {
                TestNewsItem(kvp.Value);
            }
        }
        
        [Fact]
        public void ReadsBBCNews()
        {
            var reader = new BBCNewsReader(_loggingFixture.GetLogger<BBCNewsReader>());

            TestNewsReader(reader);
        }
    }
}