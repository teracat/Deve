using Microsoft.Extensions.DependencyInjection;
using Deve.Core;
using Deve.Data;
using Deve.Logging;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients;

public static class SampleExecutorsClient
{
    public static async Task Sdk(IDataOptions options, ILog log, CancellationToken cancellationToken)
    {
        using var hander = new LogLoggingHandler(log);
        using var data = SdkBuilder.Create(EnvironmentType.Staging, options, hander);
        await Execute(data, log, cancellationToken);
    }

    public static async Task Embedded(IDataOptions options, ILog log, CancellationToken cancellationToken)
    {
        var services = new ServiceCollection();
        _ = services.AddCoreEmbedded(log, options)
                    .AddConfigurationAppSettings();
        await using var serviceProvider = services.BuildServiceProvider();
        var data = serviceProvider.GetRequiredService<IData>();
        await Execute(data, log, cancellationToken);
    }

    public static async Task Execute(IData data, ILog log, CancellationToken cancellationToken)
    {
        var sample = new SampleClient(data, log);
        await sample.Execute(cancellationToken);
    }
}
