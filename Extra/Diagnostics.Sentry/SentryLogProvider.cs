using System.Globalization;

namespace Deve.Logging;

/// <summary>
/// Use Sentry as a log provider
/// https://sentry.io/
/// </summary>
internal sealed class SentryLogProvider : ILogProvider
{
    public void Debug(string text) => SentrySdk.CaptureMessage(text, SentryLevel.Debug);

    public void Debug(string format, params object?[] args)
    {
        var formatIndexed = Utils.ConvertNameArgumentsToIndexed(format);
        string text;

        // string.Format does not handle null args well
        if (args is null)
        {
#pragma warning disable S3878 // Arrays should not be created for params parameters
            text = string.Format(CultureInfo.InvariantCulture, formatIndexed, [null]);
#pragma warning restore S3878 // Arrays should not be created for params parameters
        }
        else
        {
            text = string.Format(CultureInfo.InvariantCulture, formatIndexed, args);
        }

        _ = SentrySdk.CaptureMessage(text, SentryLevel.Debug);
    }

    public void Error(string text) => SentrySdk.CaptureMessage(text, SentryLevel.Error);

    public void Error(Exception exception) => SentrySdk.CaptureException(exception);

    public void Error(Exception exception, string message) => _ = SentrySdk.CaptureException(exception, scope => scope.SetExtra("Message", message));

    public void Error(string format, params object?[] args)
    {
        var formatIndexed = Utils.ConvertNameArgumentsToIndexed(format);
        string text;

        // string.Format does not handle null args well
        if (args is null)
        {
#pragma warning disable S3878 // Arrays should not be created for params parameters
            text = string.Format(CultureInfo.InvariantCulture, formatIndexed, [null]);
#pragma warning restore S3878 // Arrays should not be created for params parameters
        }
        else
        {
            text = string.Format(CultureInfo.InvariantCulture, formatIndexed, args);
        }

        _ = SentrySdk.CaptureMessage(text, SentryLevel.Error);
    }
}

public static class SentryLogProviderExtension
{
    private static SentryLogProvider? _instance;

    public static void AddSentry(this LogProviders logProviders)
    {
        if (_instance is null)
        {
            _instance = new SentryLogProvider();
            _ = logProviders.Add(_instance);
        }
    }

    public static void RemoveSentry(this LogProviders logProviders)
    {
        if (_instance is not null)
        {
            _ = logProviders.Remove(_instance);
            _instance = null;
        }
    }
}
