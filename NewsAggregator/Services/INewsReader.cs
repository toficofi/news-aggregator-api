using System.Collections.Generic;
using NewsAggregator.Models;

namespace NewsAggregator.Services 
{
    public interface INewsReader 
    {
        bool TryReadNewsItems(out List<NewsItem> newsItems);
    }
}