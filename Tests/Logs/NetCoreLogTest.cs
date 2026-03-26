using Microsoft.Extensions.Logging;
using Deve.Logging;

namespace Deve.Tests.Logs;

public class NetCoreLogTest : BaseLogTest
{
    public NetCoreLogTest()
        : base(CreateProvider())
    {
    }

    private static NetCoreLog CreateProvider()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return new NetCoreLog(loggerFactory.CreateLogger("Tests"));
    }
}
