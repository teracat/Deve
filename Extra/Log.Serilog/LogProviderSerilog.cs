using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Deve.Logging
{
    /// <summary>
    /// Use Serilog as a log provider
    /// https://github.com/serilog/serilog/
    /// </summary>
    internal class LogProviderSerilog : ILogProvider
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                                .AddJsonFile("Serilog.json", optional: true, reloadOnChange: true)
                                                                .Build();
        private static readonly Logger Logger = new LoggerConfiguration()
                                                .ReadFrom.Configuration(Configuration)
                                                .CreateLogger();

        public void Debug(string text)
        {
            Logger.Debug(text);
        }

        public void Debug(string format, params object[] args)
        {
            Logger.Debug(format, args);
        }

        public void Error(string text)
        {
            Logger.Error(text);
        }

        public void Error(Exception exception)
        {
            Logger.Error(exception, string.Empty);
        }

        public void Error(Exception exception, string message)
        {
            Logger.Error(exception, message);
        }

        public void Error(string format, params object[] args)
        {
            Logger.Error(format, args);
        }
    }

    public static class LogProviderSerilogExtension
    {
        private static LogProviderSerilog? _instance;

        public static void AddSerilog(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderSerilog();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveSerilog(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
