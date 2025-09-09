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
        #region Fields
        private readonly ILog _logger;
        #endregion

        #region Constructors
        public LogProviderLog4net()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            _logger = LogManager.GetLogger(typeof(LogProviderLog4net));
        }

        public LogProviderLog4net(ILog logger)
        {
            _logger = logger;
        }
        #endregion

        #region ILogProvider
        public void Debug(string text)
        {
            _logger.Debug(text);
        }

        public void Debug(string format, params object[] args)
        {
            // Log4net does not accept name arguments, so we need to convert it to indexed arguments
            _logger.DebugFormat(Utils.ConvertNameArgumentsToIndexed(format), args);
        }

        public void Error(string text)
        {
            _logger.Error(text);
        }

        public void Error(Exception exception)
        {
            _logger.Error(null, exception);
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(message, exception);
        }

        public void Error(string format, params object[] args)
        {
            // Log4net does not accept name arguments, so we need to convert it to indexed arguments
            _logger.ErrorFormat(Utils.ConvertNameArgumentsToIndexed(format), args);
        }
        #endregion
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
