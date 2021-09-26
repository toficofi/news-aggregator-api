using System.Collections.Generic;
using System.Linq;
using NewsAggregator.Models;

namespace NewsAggregator.NewsReader
{
    public static class NewsCombiner 
    {
        public static IEnumerable<NewsItem> CombineSources(HashSet<string> excludeIds, params IEnumerable<NewsItem>[] others) 
        {
            var result = new List<NewsItem>();
            var existingIds = new HashSet<string>(excludeIds);

            foreach (var otherSource in others)
            {
                foreach (var item in otherSource)
                {
                    if (existingIds.Contains(item.Id)) continue;
                    result.Add(item);
                }
            }

            return result;
        }
    }
}