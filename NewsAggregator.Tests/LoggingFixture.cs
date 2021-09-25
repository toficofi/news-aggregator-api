using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NewsAggregator.Tests 
{
    public sealed class LoggingFixture: IDisposable
    {
        private readonly ILoggerFactory _factory;
        private ServiceProvider _serviceProvider;

        public LoggingFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(b => b.AddDebug())
                .BuildServiceProvider();

            _factory = _serviceProvider.GetService<ILoggerFactory>();
        }

        public ILogger<T> GetLogger<T>() 
        {
            return _factory.CreateLogger<T>();
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }
    }
}