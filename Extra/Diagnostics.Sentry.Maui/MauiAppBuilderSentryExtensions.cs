namespace Deve.Diagnostics
{
    public static class MauiAppBuilderSentryExtensions
    {
        public static MauiAppBuilder AddDiagnosticsSentry(this MauiAppBuilder builder, string dsn)
        {
            builder.UseSentry(options =>
            {
                options.Dsn = dsn;
                options.ConfigureSentry();
            });

            return builder;
        }
    }
}
