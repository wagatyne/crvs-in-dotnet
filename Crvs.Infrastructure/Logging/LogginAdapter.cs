using Crvs.Core.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Infrastructure.Logging
{
    public class LoggingAdapter<T> : IAppLogger<T> 
    {
        private readonly ILogger<T> _logger;
        public LoggingAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger?.LogError(message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
