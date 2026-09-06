using System.Globalization;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Deve.Logging;

/// <summary>
/// Use log4net as a log provider
/// https://logging.apache.org/log4net/
/// </summary>
public sealed class Log4NetLog : ILog
{
    #region Fields
    private readonly log4net.ILog _logger;
    #endregion

    #region Constructors
    public Log4NetLog()
    {
        var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
        _ = XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        _logger = LogManager.GetLogger(typeof(Log4NetLog));
    }

    public Log4NetLog(log4net.ILog logger)
    {
        _logger = logger;
    }
    #endregion

    #region ILogProvider
    public void Debug(string text) => _logger.Debug(text);

    public void Debug(string format, params object?[] args) =>
        // Log4net does not accept name arguments, so we need to convert it to indexed arguments
        _logger.DebugFormat(CultureInfo.InvariantCulture, Utils.ConvertNameArgumentsToIndexed(format), args);

    public void Error(string text) => _logger.Error(text);

    public void Error(Exception exception) => _logger.Error(null, exception);

    public void Error(Exception exception, string message) => _logger.Error(message, exception);

    public void Error(string format, params object?[] args) =>
        // Log4net does not accept name arguments, so we need to convert it to indexed arguments
        _logger.ErrorFormat(CultureInfo.InvariantCulture, Utils.ConvertNameArgumentsToIndexed(format), args);
    #endregion
}

public static class Log4NetLogExtension
{
    private static ILog? _instance;

    public static void AddLog4Net(this MultiLog log)
    {
        if (_instance is null)
        {
            _instance = new Log4NetLog();
            log.Add(_instance);
        }
    }

    public static void RemoveLog4Net(this MultiLog log)
    {
        if (_instance is not null)
        {
            _ = log.Remove(_instance);
            _instance = null;
        }
    }
}