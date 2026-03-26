using Microsoft.Extensions.Logging;

namespace Deve.Logging;

/// <summary>
/// Use ILogger from ASP.NET Core as a log provider.
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0
/// </summary>
public sealed class NetCoreLog : ILog
{
    #region Fields
    private readonly ILogger _logger;
    #endregion

    #region Constructors
    public NetCoreLog(ILogger logger)
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

public static class NetCoreLogExtension
{
    private static ILog? _instance;

    public static void AddNetCore(this MultiLog log, ILogger logger)
    {
        if (_instance is null)
        {
            _instance = new NetCoreLog(logger);
            log.Add(_instance);
        }
    }

    public static void RemoveNetCore(this MultiLog log)
    {
        if (_instance is not null)
        {
            _ = log.Remove(_instance);
            _instance = null;
        }
    }
}
