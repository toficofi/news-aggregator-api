using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NewsAggregator.DbContexts;
using NewsAggregator.Models;
using NewsAggregator.NewsReader;
using NewsAggregator.Services;
using Xunit;

namespace NewsAggregator.Tests
{
    public class NewsUpdaterTest: IClassFixture<LoggingFixture>
    {
        LoggingFixture _loggingFixture;

        public NewsUpdaterTest(LoggingFixture loggingFixture) 
        {
            _loggingFixture = loggingFixture;
        }

        [Fact]
        public async void UpdatesNews()
        {
            var logger = _loggingFixture.GetLogger<NewsUpdateService>();
            var options = new DbContextOptionsBuilder<NewsDbContext>()
            .UseInMemoryDatabase(databaseName: "NewsAggregator")
            .Options;

            using (var context = new NewsDbContext(options))
            {
                context.News.Add(new NewsItem { Id = "mockItem1"});
                context.News.Add(new NewsItem { Id = "mockItem2"});
                context.News.Add(new NewsItem { Id = "mockItem3"});
                context.SaveChanges();
            }

            using (var context = new NewsDbContext(options)) 
            {
                var source1 = new Mock<INewsReader>();
                var source1Items = new List<NewsItem> 
                {
                    new NewsItem { Id = "mockItem1"},    
                    new NewsItem { Id = "mockItem4"},    
                    new NewsItem { Id = "mockItem5"}    
                };

                source1.Setup(n => n.TryReadNewsItems(out source1Items)).Returns(true);

                var source2 = new Mock<INewsReader>();
                var source2Items = new List<NewsItem> 
                {
                    new NewsItem { Id = "mockItem6"},    
                    new NewsItem { Id = "mockItem7"},    
                    new NewsItem { Id = "mockItem8"}    
                };

                source2.Setup(n => n.TryReadNewsItems(out source2Items)).Returns(true);

                var updateServices = new NewsUpdateService(logger, context, source1.Object, source2.Object);

                await updateServices.RequestUpdate();

                Assert.Equal(8, context.News.Count());
            }
        }
    }
}