using Microsoft.Extensions.Logging;

namespace Deve.Logging
{
    /// <summary>
    /// Use ILogger from ASP.NET Core as a log provider.
    /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0
    /// </summary>
    internal class LogProviderNetCore : ILogProvider
    {
        #region Fields
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        public LogProviderNetCore(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region ILogProvider
        public void Debug(string text) => _logger.LogDebug(text);

        public void Debug(string format, params object?[] args) => _logger.LogDebug(format, args);

        public void Error(string text) => _logger.LogError(text);

        public void Error(Exception exception) => _logger.LogError(exception, string.Empty);

        public void Error(Exception exception, string message) => _logger.LogError(exception, message);

        public void Error(string format, params object?[] args) => _logger.LogError(format, args);
        #endregion
    }

    public static class LogProviderNetCoreExtension
    {
        private static LogProviderNetCore? _instance;

        public static void AddNetCore(this LogProviders logProviders, ILogger logger)
        {
            if (_instance is null)
            {
                _instance = new LogProviderNetCore(logger);
                _ = logProviders.Add(_instance);
            }
        }

        public static void RemoveNetCore(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                _ = logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
