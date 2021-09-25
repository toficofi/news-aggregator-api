namespace NewsAggregator.Models 
{
    public sealed class NewsSource 
    {
        public string Title { get; set;}
        public string Thumbnail { get; set; }
        public string Copyright { get; set; }
    }

    public sealed class NewsItem 
    {
        public string Id { get; set;}
        public NewsSource Source;

    }
}