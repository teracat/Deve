using Deve.Logging;

namespace Deve.Tests.Logs;

public class TraceLogTest : BaseLogTest
{
    public TraceLogTest()
        : base(new LogProviderTrace())
    {
    }
}
