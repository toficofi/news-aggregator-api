using System.Collections.Generic;
using System.Linq;
using NewsAggregator.Models;

namespace NewsAggregator.Services
{
    public sealed class NewsCombiner 
    {
        private readonly IEnumerable<NewsItem> _combineSource;
        
        public NewsCombiner(IEnumerable<NewsItem> combineSource) 
        {
            _combineSource = combineSource;
        }

        public IEnumerable<NewsItem> CombineWith(params IEnumerable<NewsItem>[] others) 
        {
            var result = new List<NewsItem>();
            var existingIds = new HashSet<string>();

            foreach (var item in _combineSource)
            {
                existingIds.Add(item.Id);
                result.Add(item);
            }

            foreach (var otherSource in others)
            {
                foreach (var item in otherSource)
                {
                    if (existingIds.Contains(item.Id)) continue;
                    result.Add(item);
                }
            }
            
            return result.OrderBy(i => i.Date).ToList();
        }
    }
}