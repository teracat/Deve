namespace Deve.Sdk
{
    /// <summary>
    /// Write the Http requests and responses to the Console.
    /// </summary>
    public class LoggingHandlerConsole : LoggingHandlerBase
    {
        public LoggingHandlerConsole(string outputPrefix = "")
            : base(outputPrefix)
        {
        }

        public LoggingHandlerConsole(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
