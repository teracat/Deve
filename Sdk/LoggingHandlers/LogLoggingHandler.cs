using Deve.Logging;

namespace Deve.Sdk.LoggingHandlers;

/// <summary>
/// Write the Http requests and responses to the Log.
/// </summary>
public class LogLoggingHandler : BaseLoggingHandler
{
    private readonly ILog _log;

    public LogLoggingHandler(ILog log)
    {
        _log = log;
    }

    public LogLoggingHandler(string outputPrefix, ILog log)
        : base(outputPrefix)
    {
        _log = log;
    }

    public LogLoggingHandler(HttpMessageHandler innerHandler, ILog log)
        : base(innerHandler)
    {
        _log = log;
    }

    public LogLoggingHandler(HttpMessageHandler innerHandler, string outputPrefix, ILog log)
        : base(innerHandler, outputPrefix)
    {
        _log = log;
    }

    protected override void Write(string text) => _log.Debug(text);
}
