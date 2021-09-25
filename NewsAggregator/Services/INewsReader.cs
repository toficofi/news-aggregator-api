using System.Collections.Generic;
using NewsAggregator.Models;

namespace NewsAggregator.Services 
{
    public interface INewsReader 
    {
        public bool TryReadNewsItems(out Dictionary<string, NewsItem> newsItems);
    }
}