using Serilog;
using Serilog.Sinks.InMemory;
using Deve.Logging;

namespace Deve.Tests.Logs;

public class SerilogLogTest : BaseLogTest
{
    public SerilogLogTest()
        : base(CreateProvider())
    {
    }

    private static SerilogLogProvider CreateProvider() =>
        new(new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.InMemory()
            .CreateLogger());
}
