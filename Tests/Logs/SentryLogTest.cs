using Deve.Logging;

namespace Deve.Tests.Logs;

public class SentryLogTest : BaseLogTest
{
    public SentryLogTest()
        : base(CreateProvider())
    {
    }

    private static SentryLogProvider CreateProvider() => new();
}
