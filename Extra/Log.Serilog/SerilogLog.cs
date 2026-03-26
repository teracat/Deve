using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Deve.Logging;

/// <summary>
/// Use Serilog as a log provider
/// https://github.com/serilog/serilog/
/// </summary>
public sealed class SerilogLog : ILog
{
    #region Fields
    private readonly Logger _logger;
    #endregion

    #region Constructors
    public SerilogLog()
    {
        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Serilog.json", optional: true, reloadOnChange: true)
                            .Build();
        _logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();
    }

    public SerilogLog(IConfiguration configuration)
    {
        _logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();
    }

    public SerilogLog(Logger logger)
    {
        _logger = logger;
    }
    #endregion

    #region ILogProvider
    public void Debug(string text) => _logger.Debug(text);

    public void Debug(string format, params object?[] args) => _logger.Debug(format, args);

    public void Error(string text) => _logger.Error(text);

    public void Error(Exception exception) => _logger.Error(exception, string.Empty);

    public void Error(Exception exception, string message) => _logger.Error(exception, message);

    public void Error(string format, params object?[] args) => _logger.Error(format, args);
    #endregion
}

public static class SerilogLogExtension
{
    private static ILog? _instance;

    public static void AddSerilog(this MultiLog log)
    {
        if (_instance is null)
        {
            _instance = new SerilogLog();
            log.Add(_instance);
        }
    }

    public static void RemoveSerilog(this MultiLog log)
    {
        if (_instance is not null)
        {
            _ = log.Remove(_instance);
            _instance = null;
        }
    }
}
