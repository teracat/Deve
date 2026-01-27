using Deve.Logging;

namespace Deve.Tests.Logs;

public class ConsoleLogTest : BaseLogTest
{
    public ConsoleLogTest()
        : base(new LogProviderConsole())
    {
    }
}
