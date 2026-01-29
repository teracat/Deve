using Deve.Core;
using Deve.Data;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients;

public static class SampleExecutorsClient
{
    public static async Task Sdk(IDataOptions options)
    {
        using var hander = new LogLoggingHandler();
        using var data = SdkBuilder.Create(EnvironmentType.Staging, options, hander);
        await Execute(data);
    }

    public static async Task Embedded(IDataOptions options)
    {
        using var data = CoreBuilder.CreateEmbedded(options);
        await Execute(data);
    }

    public static async Task Execute(IData data)
    {
        var sample = new SampleClient(data);
        await sample.Execute();
    }
}
