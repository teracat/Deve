using NLog;
using NLog.Config;
using NLog.Targets;
using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogNLog : TestLogBase
    {
        public TestLogNLog()
            : base(CreateProvider())
        {
        }

        private static LogProviderNLog CreateProvider()
        {
            var config = new LoggingConfiguration();

            var memory = new MemoryTarget("mem") { Layout = "${longdate}|${level}|${message}" };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, memory);

            LogManager.Configuration = config;

            var logger = LogManager.GetLogger("test");

            return new LogProviderNLog(logger);
        }
    }
}
