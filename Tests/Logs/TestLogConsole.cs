using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogConsole : TestLogBase
    {
        public TestLogConsole()
            : base(new LogProviderConsole())
        {
        }
    }
}
