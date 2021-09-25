using System.ServiceModel.Syndication;
using System.Xml;
using NewsAggregator.Settings;

namespace NewsAggregator.Services
{
    public sealed class FeedReader
    {
        private string _url = "http://feeds.bbci.co.uk/news/uk/rss.xml";

        public FeedReader() 
        {
            XmlReader reader = XmlReader.Create(_url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
        }
    }
}