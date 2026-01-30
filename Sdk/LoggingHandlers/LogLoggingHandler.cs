using Deve.Logging;

namespace Deve.Sdk.LoggingHandlers;

/// <summary>
/// Write the Http requests and responses to the Log.
/// </summary>
public class LogLoggingHandler : BaseLoggingHandler
{
    public LogLoggingHandler()
    {
    }

    public LogLoggingHandler(string outputPrefix)
        : base(outputPrefix)
    {
    }

    public LogLoggingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    public LogLoggingHandler(HttpMessageHandler innerHandler, string outputPrefix)
        : base(innerHandler, outputPrefix)
    {
    }

    protected override void Write(string text) => Log.Debug(text);
}