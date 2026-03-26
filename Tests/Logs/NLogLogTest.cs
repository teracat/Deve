using NLog;
using NLog.Config;
using NLog.Targets;
using Deve.Logging;

namespace Deve.Tests.Logs;

public class NLogLogTest : BaseLogTest
{
    public NLogLogTest()
        : base(CreateProvider())
    {
    }

    private static NLogLog CreateProvider()
    {
        var config = new LoggingConfiguration();

        using var memory = new MemoryTarget("mem") { Layout = "${longdate}|${level}|${message}" };
        config.AddRule(LogLevel.Info, LogLevel.Fatal, memory);

        LogManager.Configuration = config;

        var logger = LogManager.GetLogger("test");

        return new NLogLog(logger);
    }
}
