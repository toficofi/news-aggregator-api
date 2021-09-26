using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsAggregator.DbContexts;
using NewsAggregator.NewsReader;
using NewsAggregator.Settings;

namespace NewsAggregator.Services
{
    public class NewsUpdateService
    {
        private readonly ILogger _logger;
        private readonly INewsReader[] _readers;
        private NewsDbContext _newsDb;
        
        public NewsUpdateService(ILogger logger, NewsDbContext newsDb, params INewsReader[] readers)
        {
            _logger = logger;
            _readers = readers;
            _newsDb = newsDb;
        }

        public async Task RequestUpdate()
        {
            
        }
    }
}