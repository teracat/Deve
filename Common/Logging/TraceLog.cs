using System.Diagnostics;

namespace Deve.Logging;

/// <summary>
/// A logging provider that sends log messages to the trace output.
/// </summary>
public sealed class TraceLog : ILog
{
    /// <inheritdoc/>
    public void Debug(string text) => Trace.TraceInformation(text);

    /// <inheritdoc/>
    public void Debug(string format, params object?[] args) =>
        // Trace does not accept name arguments, so we need to convert it to indexed arguments
        Trace.TraceInformation(Utils.ConvertNameArgumentsToIndexed(format), args);

    /// <inheritdoc/>
    public void Error(string text) => Trace.TraceError(text);

    /// <inheritdoc/>
    public void Error(Exception exception)
    {
        if (exception is null)
        {
            return;
        }

        Trace.TraceError(exception.ToString());
    }

    /// <inheritdoc/>
    public void Error(Exception exception, string message) => Trace.TraceError("{0} --> {1}", message, exception);

    /// <inheritdoc/>
    public void Error(string format, params object?[] args) =>
        // Trace does not accept name arguments, so we need to convert it to indexed arguments
        Trace.TraceError(Utils.ConvertNameArgumentsToIndexed(format), args);
}

public static class TraceLogExtension
{
    private static ILog? _instance;

    public static void AddTrace(this MultiLog log)
    {
        if (_instance is null)
        {
            _instance = new TraceLog();
            log.Add(_instance);
        }
    }

    public static void RemoveTrace(this MultiLog log)
    {
        if (_instance is not null)
        {
            _ = log.Remove(_instance);
            _instance = null;
        }
    }
}
