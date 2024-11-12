using Microsoft.Extensions.Logging;

namespace Deve
{
    /// <summary>
    /// Use ILogger from ASP.NET Core as a log provider.
    /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0
    /// </summary>
    internal class LogProviderNetCore : ILogProvider
    {
        private ILogger _logger;

        public LogProviderNetCore(ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string text)
        {
            _logger.LogDebug(text);
        }

        public void Error(string text)
        {
            _logger.LogError(text);
        }
    }

    public static class LogProviderNetCoreExtension
    {
        private static LogProviderNetCore? _instance;

        public static void AddNetCore(this LogProviders logProviders, ILogger logger)
        {
            if (_instance is null)
            {
                _instance = new LogProviderNetCore(logger);
                logProviders.Add(_instance);
            }
        }

        public static void RemoveNetCore(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
