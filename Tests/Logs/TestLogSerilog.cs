using Serilog;
using Serilog.Sinks.InMemory;
using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogSerilog : TestLogBase
    {
        public TestLogSerilog()
            : base(CreateProvider())
        {
        }

        private static LogProviderSerilog CreateProvider()
        {
            return new LogProviderSerilog(new LoggerConfiguration()
                                          .MinimumLevel.Verbose()
                                          .WriteTo.InMemory()
                                          .CreateLogger());
        }
    }
}
