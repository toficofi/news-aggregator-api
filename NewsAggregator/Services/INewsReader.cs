using System.Collections.Generic;
using NewsAggregator.Models;

namespace NewsAggregator.Services 
{
    public enum NewsReaderSource
    {
        BBC
    }

    public interface INewsReader 
    {
        NewsReaderSource Source { get; }
        bool TryReadNewsItems(out List<NewsItem> newsItems);
    }
}