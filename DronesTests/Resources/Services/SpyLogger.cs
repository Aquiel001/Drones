using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesTests.Resources.Services
{
    public class SpyLogger<T> : ILogger<T>
    {
        private readonly ITestLogger _logger;
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
            //return NullLogger.Instance.dis;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            NullLogger.Instance.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
