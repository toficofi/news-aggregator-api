using System;
using System.Collections.Generic;
using NewsAggregator.Models;
using NewsAggregator.NewsReader;
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
            var ignoreIds = new HashSet<string> {
                "mockItem1",
                "mockItem2"
            };

            var toMerge1 = new List<NewsItem> 
            {
                new NewsItem 
                {
                    Id = "mockItem2",
                    Date = new DateTime(2021, 12, 6)
                },
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

            var result = NewsCombiner.CombineSources(ignoreIds, toMerge1, toMerge2);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            Assert.Collection(result, 
                item => item.Id = "mockitem3",
                item => item.Id = "mockItem4",
                item => item.Id = "mockItem5",
                item => item.Id = "mockItem6"
            );
        }
    }
}