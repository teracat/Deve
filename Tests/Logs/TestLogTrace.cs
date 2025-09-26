using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogTrace : TestLogBase
    {
        public TestLogTrace()
            : base(new LogProviderTrace())
        {
        }
    }
}
