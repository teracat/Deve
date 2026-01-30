using Microsoft.Extensions.Logging;
using Deve.Logging;

namespace Deve.Tests.Logs;

public class NetCoreLogTest : BaseLogTest
{
    public NetCoreLogTest()
        : base(CreateProvider())
    {
    }

    private static NetCoreLogProvider CreateProvider()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return new NetCoreLogProvider(loggerFactory.CreateLogger("Tests"));
    }
}
