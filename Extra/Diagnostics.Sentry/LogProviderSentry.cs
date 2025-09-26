namespace Deve.Logging
{
    /// <summary>
    /// Use Sentry as a log provider
    /// https://sentry.io/
    /// </summary>
    internal class LogProviderSentry : ILogProvider
    {
        #region ILogProvider
        public void Debug(string text)
        {
            SentrySdk.CaptureMessage(text, SentryLevel.Debug);
        }

        public void Debug(string format, params object[] args)
        {
            var formatIndexed = Utils.ConvertNameArgumentsToIndexed(format);
            string text;

            // string.Format does not handle null args well
            if (args is null)
            {
                text = string.Format(formatIndexed, [null]);
            }
            else
            {
                text = string.Format(formatIndexed, args);
            }

            SentrySdk.CaptureMessage(text, SentryLevel.Debug);
        }

        public void Error(string text)
        {
            SentrySdk.CaptureMessage(text, SentryLevel.Error);
        }

        public void Error(Exception exception)
        {
            SentrySdk.CaptureException(exception);
        }

        public void Error(Exception exception, string message)
        {
            SentrySdk.CaptureException(exception, scope =>
            {
                scope.SetExtra("Message", message);
            });
        }

        public void Error(string format, params object[] args)
        {
            var formatIndexed = Utils.ConvertNameArgumentsToIndexed(format);
            string text;

            // string.Format does not handle null args well
            if (args is null)
            {
                text = string.Format(formatIndexed, [null]);
            }
            else
            {
                text = string.Format(formatIndexed, args);
            }

            SentrySdk.CaptureMessage(text, SentryLevel.Error);
        }
        #endregion
    }

    public static class LogProviderSentryExtension
    {
        private static LogProviderSentry? _instance;

        public static void AddSentry(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderSentry();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveSentry(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
