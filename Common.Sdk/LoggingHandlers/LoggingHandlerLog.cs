using Deve.Logging;

namespace Deve.Sdk.LoggingHandlers
{
    /// <summary>
    /// Write the Http requests and responses to the Log.
    /// </summary>
    public class LoggingHandlerLog : LoggingHandlerBase
    {
        public LoggingHandlerLog()
        {
        }

        public LoggingHandlerLog(string outputPrefix)
            : base(outputPrefix)
        {
        }

        public LoggingHandlerLog(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        public LoggingHandlerLog(HttpMessageHandler innerHandler, string outputPrefix)
            : base(innerHandler, outputPrefix)
        {
        }

        protected override void Write(string text)
        {
            Log.Debug(text);
        }
    }
}