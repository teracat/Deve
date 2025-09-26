namespace Deve
{
    public static class MauiAppBuilderSentryExtensions
    {
        public static MauiAppBuilder UseSentryForMaui(this MauiAppBuilder builder, string dsn)
        {
            builder.UseSentry(options =>
            {
                options.ConfigureSentry(dsn);
            });

            return builder;
        }
    }
}
