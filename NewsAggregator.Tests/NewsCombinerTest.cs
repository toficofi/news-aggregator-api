using System;
using System.Collections.Generic;
using System.Linq;
using NewsAggregator.Models;
using NewsAggregator.Services;
using NewsAggregator.Tests.Mocks;
using Xunit;

namespace NewsAggregator.Tests
{
    public sealed class NewsCombinerTest
    {
        [Fact]
        public void CombinesNewsSources()
        {
            var originalSource = new List<NewsItem> 
            {
                new NewsItem 
                {
                    Id = "mockItem3",
                    Date = new DateTime(2021, 12, 7)
                },
                new NewsItem 
                {
                    Id = "mockItem1",
                    Date = new DateTime(2021, 12, 5)
                },
                new NewsItem 
                {
                    Id = "mockItem2",
                    Date = new DateTime(2021, 12, 6)
                },
            };

            var toMerge1 = new List<NewsItem> 
            {
                new NewsItem 
                {
                    Id = "mockItem1",
                    Date = new DateTime(2021, 12, 5)
                },
                new NewsItem 
                {
                    Id = "mockItem4",
                    Date = new DateTime(2021, 12, 8)
                },
            };

            var toMerge2 = new List<NewsItem> 
            {
                new NewsItem 
                {
                    Id = "mockItem6",
                    Date = new DateTime(2021, 12, 10)
                },
                new NewsItem 
                {
                    Id = "mockItem5",
                    Date = new DateTime(2021, 12, 9)
                }
            };

            var combiner = new NewsCombiner(originalSource);
            var result = combiner.CombineWith(toMerge1, toMerge2);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            var expectedIdOrder = new[]
            {
                "mockItem1",
                "mockItem2",
                "mockItem3",
                "mockItem4",
                "mockItem5",
                "mockItem6"
            };

            Assert.True(result.Select(i => i.Id).SequenceEqual(expectedIdOrder));
        }
    }
}