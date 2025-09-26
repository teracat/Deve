namespace Deve
{
    public static class SentryOptionsExtensions
    {
        public static void ConfigureSentry(this SentryOptions options, string dsn)
        {
            options.Dsn = dsn;

            // Enable logs to be sent to Sentry
#pragma warning disable SENTRY0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            options.Experimental.EnableLogs = true;
#pragma warning restore SENTRY0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#if DEBUG
            // When configuring for the first time, to see what the SDK is doing:
            options.Debug = true;

            // To see all events (useful in development, but not recommended in production):
            options.TracesSampleRate = 1.0;
#else
                // To sample traces (recommended in production):
                o.TracesSampleRate = 0.2;
#endif
        }
    }
}
