using System.Collections.Generic;
using NewsAggregator.Models;
using NewsAggregator.NewsReader;
using NewsAggregator.Services;

namespace NewsAggregator.Tests.Mocks
{
    public sealed class MockNewsReader : INewsReader
    {
        private List<NewsItem> _items;

        public MockNewsReader(List<NewsItem> items)
        {
            _items = items;
        }

        public NewsReaderSource Source => throw new System.NotImplementedException();

        public bool TryReadNewsItems(out List<NewsItem> newsItems)
        {
            newsItems = _items;
            return true;
        }
    }
}