using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Extensions.Logging;
using NewsAggregator.Models;
using NewsAggregator.Settings;

namespace NewsAggregator.Services
{
    public sealed class BBCNewsReader: INewsReader
    {
        private const string URL = "http://feeds.bbci.co.uk/news/uk/rss.xml";
        private readonly ILogger _logger;

        public BBCNewsReader(ILogger<BBCNewsReader> logger) 
        {
            _logger = logger;
        }

        public bool TryReadNewsItems(out Dictionary<string, NewsItem> newsItems)
        {
            newsItems = new Dictionary<string, NewsItem>();

            _logger.LogInformation($"Reading BBC feed from {URL}");

            try 
            {
                newsItems = ReadNewsItems();
                return true;
            } 
            catch (Exception e) 
            {
                _logger.LogError("Failed to read BBC feed!", e);
                return false;
            }
        }

        private Dictionary<string, NewsItem> ReadNewsItems() 
        {
            var newsItems = new Dictionary<string, NewsItem>();

            XmlReader reader = XmlReader.Create(URL);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            var newsSource = new NewsSource 
            {
                Title = feed.Title.Text,
                ThumbnailUrl = feed.ImageUrl.AbsoluteUri,
                Copyright = feed.Copyright.Text
            };

            foreach (var feedItem in feed.Items)
            {
                var newsItem = new NewsItem
                 {
                    Source = newsSource,
                    Id = feedItem.Id,
                    Headline = feedItem.Title.Text,
                    Summary = feedItem.Summary.Text,
                    Date = feedItem.PublishDate.UtcDateTime,
                };

                if (feedItem.Links == null || feedItem.Links.Count == 0) 
                {
                    _logger.LogInformation($"Skipping item {feedItem.Id} because it is missing Links");
                    continue;
                }

                newsItem.Url = feedItem.Links[0].Uri.AbsoluteUri;

                newsItems.Add(newsItem.Id, newsItem);
            }

            _logger.LogInformation($"Loaded {newsItems.Count}/{feed.Items.Count()} from feed");

            return newsItems;
        }
    }
}