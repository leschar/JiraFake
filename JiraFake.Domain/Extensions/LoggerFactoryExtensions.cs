using Microsoft.Extensions.Logging;

namespace JiraFake.Domain.Extensions
{
    public static class LoggerFactoryExtensions
    {
        public static ILogger CreateLogger(this ILoggerFactory loggerFactory, string category)
        {
            return new LoggerWrapper(loggerFactory.CreateLogger(category));
        }
    }

    public class LoggerWrapper : ILogger
    {
        private readonly ILogger _logger;

        public LoggerWrapper(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IDisposable BeginScope<TState>(TState state) => _logger.BeginScope(state);

        public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
