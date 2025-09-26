using Microsoft.AspNetCore.Hosting;

namespace Deve
{
    public static class WebHostBuilderSentryExtensions
    {
        public static IWebHostBuilder UseSentryForAspNetCore(this IWebHostBuilder builder, string dsn)
        {
            builder.UseSentry(options =>
            {
                options.ConfigureForDeve(dsn);
            });

            return builder;
        }
    }
}
