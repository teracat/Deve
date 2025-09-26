using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogDebug : TestLogBase
    {
        public TestLogDebug()
            : base(new LogProviderDebug())
        {
        }
    }
}
