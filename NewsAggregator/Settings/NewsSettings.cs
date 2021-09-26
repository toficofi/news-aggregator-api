using System.Collections.Generic;
using NewsAggregator.NewsReader;

namespace NewsAggregator.Settings 
{
    public sealed class NewsSettings 
    {
        public List<NewsReaderSource> Sources { get; set; }
    }
}