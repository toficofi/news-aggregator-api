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

        public bool TryReadNewsItems(out List<NewsItem> newsItems)
        {
            newsItems = null;

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

        private List<NewsItem> ReadNewsItems() 
        {
            var newsItems = new List<NewsItem>();
            var idHashset = new HashSet<string>(); // keep a track of IDs we've already used so we can detect duplicates

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
                if (idHashset.Contains(feedItem.Id)) 
                {
                    _logger.LogInformation($"Skipping item {feedItem.Id} because it is a duplicate");
                }

                
                if (feedItem.Links == null || feedItem.Links.Count == 0) 
                {
                    _logger.LogInformation($"Skipping item {feedItem.Id} because it is missing Links");
                    continue;
                }


                var newsItem = new NewsItem
                 {
                    Source = newsSource,
                    Id = feedItem.Id,
                    Headline = feedItem.Title.Text,
                    Summary = feedItem.Summary.Text,
                    Date = feedItem.PublishDate.UtcDateTime,
                };

                newsItem.Url = feedItem.Links[0].Uri.AbsoluteUri;
                
                idHashset.Add(feedItem.Id);
                newsItems.Add(newsItem);
            }

            _logger.LogInformation($"Loaded {newsItems.Count}/{feed.Items.Count()} from feed");

            return newsItems;
        }
    }
}