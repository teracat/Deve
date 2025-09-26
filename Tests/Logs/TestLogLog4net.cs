using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Deve.Logging;

namespace Deve.Tests.Logs
{
    public class TestLogLog4net : TestLogBase
    {
        public TestLogLog4net()
            : base(CreateProvider())
        {
        }

        private static LogProviderLog4net CreateProvider()
        {
            var memory = new MemoryAppender();
            memory.ActivateOptions();

            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();
            hierarchy.Root.AddAppender(memory);
            hierarchy.Root.Level = log4net.Core.Level.Debug;
            hierarchy.Configured = true;

            var logger = LogManager.GetLogger(typeof(TestLogLog4net));

            return new LogProviderLog4net(logger);
        }
    }
}
