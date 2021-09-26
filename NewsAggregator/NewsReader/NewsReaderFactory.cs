using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace NewsAggregator.NewsReader
{
    public static class NewsReaderFactory
    {
        private static Dictionary<NewsReaderSource, Func<ILogger, INewsReader>> _factories = new() {
            [NewsReaderSource.BBC] = (logger) => new BBCNewsReader(logger)
        };

        public static INewsReader Create(NewsReaderSource source, ILogger logger = null) 
        {
            if (logger == null) logger = NullLogger.Instance;
            
            return _factories[source].Invoke(logger);
        }
    }
}