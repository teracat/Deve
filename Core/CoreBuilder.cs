using Microsoft.Extensions.DependencyInjection;
using Deve.Data;

namespace Deve.Core;

public static class CoreBuilder
{
    public static IData CreateEmbedded(IDataOptions options)
    {
        var services = new ServiceCollection();
        _ = services.AddCoreEmbedded(options);
        using var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IData>();
    }
}
