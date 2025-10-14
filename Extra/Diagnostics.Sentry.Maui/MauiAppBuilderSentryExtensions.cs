namespace Deve.Diagnostics
{
    public static class MauiAppBuilderSentryExtensions
    {
        public static MauiAppBuilder AddDiagnosticsSentry(this MauiAppBuilder builder)
        {
            builder.UseSentry(options =>
            {
                options.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
                options.ConfigureSentry();
            });

            return builder;
        }
    }
}
