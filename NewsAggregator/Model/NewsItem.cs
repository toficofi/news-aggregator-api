using System;

namespace NewsAggregator.Models 
{
    public sealed class NewsSource 
    {
        public string Title { get; set;}
        public string ThumbnailUrl { get; set; }
        public string Copyright { get; set; }
    }

    public sealed class NewsItem 
    {
        public string Id { get; set; }
        public NewsSource Source { get; set; }
        public string Title { get; set;}
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}