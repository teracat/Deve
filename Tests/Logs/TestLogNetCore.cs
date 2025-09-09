using Deve.Logging;
using Microsoft.Extensions.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogNetCore : TestLogBase
    {
        public TestLogNetCore()
            : base(new LogProviderNetCore(LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("Tests")))
        {
        }
    }
}
