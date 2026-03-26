namespace Deve.Tests.Logs;

public class SentryLogTest : BaseLogTest
{
    public SentryLogTest()
        : base(CreateProvider())
    {
    }

    private static Logging.SentryLog CreateProvider() => new();
}
