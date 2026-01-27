namespace Deve.Sdk.LoggingHandlers;

/// <summary>
/// Write the Http requests and responses to the Console.
/// </summary>
public class ConsoleLoggingHandler : BaseLoggingHandler
{
    public ConsoleLoggingHandler()
    {
    }

    public ConsoleLoggingHandler(string outputPrefix)
        : base(outputPrefix)
    {
    }

    public ConsoleLoggingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    public ConsoleLoggingHandler(HttpMessageHandler innerHandler, string outputPrefix)
        : base(innerHandler, outputPrefix)
    {
    }

    protected override void Write(string text) => Console.WriteLine(text);
}