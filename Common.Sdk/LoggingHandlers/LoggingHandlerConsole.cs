namespace Deve.Sdk.LoggingHandlers
{
    /// <summary>
    /// Write the Http requests and responses to the Console.
    /// </summary>
    public class LoggingHandlerConsole : LoggingHandlerBase
    {
        public LoggingHandlerConsole()
        {
        }

        public LoggingHandlerConsole(string outputPrefix)
            : base(outputPrefix)
        {
        }

        public LoggingHandlerConsole(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        public LoggingHandlerConsole(HttpMessageHandler innerHandler, string outputPrefix)
            : base(innerHandler, outputPrefix)
        {
        }

        protected override void Write(string text) => Console.WriteLine(text);
    }
}