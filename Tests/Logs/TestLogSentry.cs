using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogSentry : TestLogBase
    {
        public TestLogSentry()
            : base(CreateProvider())
        {
        }

        private static LogProviderSentry CreateProvider() => new();
    }
}
