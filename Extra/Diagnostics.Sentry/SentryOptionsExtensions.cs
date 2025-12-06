// Uncomment the following line if you want to use OpenTelemetry integration with Sentry.
using Sentry.Infrastructure;
using Sentry.OpenTelemetry;

namespace Deve.Diagnostics
{
    public static class SentryOptionsExtensions
    {
        public static void ConfigureSentry(this SentryOptions options)
        {
            // Enable logs to be sent to Sentry
#pragma warning disable SENTRY0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            options.Experimental.EnableLogs = true;
#pragma warning restore SENTRY0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#if DEBUG
            // When configuring for the first time, to see what the SDK is doing:
            options.Debug = true;
            options.DiagnosticLevel = SentryLevel.Debug;

            // To see all events (useful in development, but not recommended in production):
            options.TracesSampleRate = 1.0;
#else
            // To sample traces (recommended in production):
            options.TracesSampleRate = 0.3;   // Adjust the sample rate as needed

            options.DiagnosticLevel = SentryLevel.Warning;
#endif

            options.DiagnosticLogger = new ConsoleDiagnosticLogger(options.DiagnosticLevel);

            // To integrate OpenTelemetry with Sentry you need to uncomment the following line and add the Sentry.OpenTelemetry package.
            // You will also need to configure OpenTelemetry in your application.
            options.UseOpenTelemetry();
        }
    }
}
