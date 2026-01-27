using Deve.Logging;

namespace Deve.Tests.Logs;

public class DebugLogTest : BaseLogTest
{
    public DebugLogTest()
        : base(new LogProviderDebug())
    {
    }
}
