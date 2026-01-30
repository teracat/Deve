using System.Globalization;
using NLog;

namespace Deve.Logging;

/// <summary>
/// Use NLog as a log provider
/// https://github.com/nlog/nlog/wiki/Tutorial
/// </summary>
internal sealed class NLogLogProvider : ILogProvider
{
    #region Fields
    private readonly Logger _logger;
    #endregion

    #region Constructors
    public NLogLogProvider()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public NLogLogProvider(Logger logger)
    {
        _logger = logger;
    }
    #endregion

    #region ILogProvider
    public void Debug(string text) => _logger.Debug(text);

    public void Debug(string format, params object?[] args) => _logger.Debug(CultureInfo.InvariantCulture, format, args);

    public void Error(string text) => _logger.Error(text);

    public void Error(Exception exception) => _logger.Error(exception, string.Empty);

    public void Error(Exception exception, string message) => _logger.Error(exception, message);

    public void Error(string format, params object?[] args) => _logger.Error(CultureInfo.InvariantCulture, format, args);
    #endregion
}

public static class NLogLogProviderExtension
{
    private static NLogLogProvider? _instance;

    public static void AddNLog(this LogProviders logProviders)
    {
        if (_instance is null)
        {
            _instance = new NLogLogProvider();
            _ = logProviders.Add(_instance);
        }
    }

    public static void RemoveNLog(this LogProviders logProviders)
    {
        if (_instance is not null)
        {
            _ = logProviders.Remove(_instance);
            _instance = null;
        }
    }
}
