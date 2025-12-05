namespace Deve.Diagnostics
{
    public static class MauiAppBuilderSentryExtensions
    {
        public static MauiAppBuilder AddDiagnosticsSentry(this MauiAppBuilder builder, string sentryDsn)
        {
            _ = builder.UseSentry(options =>
            {
                options.Dsn = sentryDsn;
                options.ConfigureSentry();
            });

            return builder;
        }
    }
}
