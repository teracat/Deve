using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Deve.Logging;

/// <summary>
/// Use Serilog as a log provider
/// https://github.com/serilog/serilog/
/// </summary>
internal sealed class SerilogLogProvider : ILogProvider
{
    #region Fields
    private readonly Logger _logger;
    #endregion

    #region Constructors
    public SerilogLogProvider()
    {
        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Serilog.json", optional: true, reloadOnChange: true)
                            .Build();
        _logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();
    }

    public SerilogLogProvider(IConfiguration configuration)
    {
        _logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();
    }

    public SerilogLogProvider(Logger logger)
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

public static class SerilogLogProviderExtension
{
    private static SerilogLogProvider? _instance;

    public static void AddSerilog(this LogProviders logProviders)
    {
        if (_instance is null)
        {
            _instance = new SerilogLogProvider();
            _ = logProviders.Add(_instance);
        }
    }

    public static void RemoveSerilog(this LogProviders logProviders)
    {
        if (_instance is not null)
        {
            _ = logProviders.Remove(_instance);
            _instance = null;
        }
    }
}
