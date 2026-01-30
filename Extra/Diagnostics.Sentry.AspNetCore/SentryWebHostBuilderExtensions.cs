using Microsoft.AspNetCore.Hosting;

namespace Deve.Diagnostics;

public static class SentryWebHostBuilderExtensions
{
    public static IWebHostBuilder AddDiagnosticsSentry(this IWebHostBuilder builder)
    {
        _ = builder.UseSentry(options => options.ConfigureSentry());

        return builder;
    }
}
