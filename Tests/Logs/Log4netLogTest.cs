using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Deve.Logging;

namespace Deve.Tests.Logs;

public class Log4netLogTest : BaseLogTest
{
    public Log4netLogTest()
        : base(CreateProvider())
    {
    }

    private static Log4NetLogProvider CreateProvider()
    {
        var memory = new MemoryAppender();
        memory.ActivateOptions();

        var hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.Root.RemoveAllAppenders();
        hierarchy.Root.AddAppender(memory);
        hierarchy.Root.Level = log4net.Core.Level.Debug;
        hierarchy.Configured = true;

        var logger = LogManager.GetLogger(typeof(Log4netLogTest));

        return new Log4NetLogProvider(logger);
    }
}
