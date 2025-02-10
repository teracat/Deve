using Deve.Logging;

namespace Deve.Sdk.LoggingHandlers
{
    /// <summary>
    /// Write the Http requests and responses to the Log.
    /// </summary>
    public class LoggingHandlerLog : LoggingHandlerBase
    {
        public LoggingHandlerLog(string outputPrefix = "")
            : base(outputPrefix)
        {
        }

        public LoggingHandlerLog(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override void Write(string text)
        {
            Log.Debug(text);
        }
    }
}