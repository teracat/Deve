using System.Reflection;
using log4net;
using log4net.Config;

namespace Deve.Logging
{
    /// <summary>
    /// Use log4net as a log provider
    /// https://logging.apache.org/log4net/
    /// </summary>
    internal class LogProviderLog4net : ILogProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LogProviderLog4net));

        public LogProviderLog4net()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public void Debug(string text)
        {
            Logger.Debug(text);
        }

        public void Error(string text)
        {
            Logger.Error(text);
        }
    }

    public static class LogProviderLog4netExtension
    {
        private static LogProviderLog4net? _instance;

        public static void AddLog4net(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderLog4net();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveLog4net(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
