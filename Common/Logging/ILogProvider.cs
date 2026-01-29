namespace Deve.Logging;

/// <summary>
/// It includes the methods to write to a log provider.
/// </summary>
public interface ILogProvider
{
    /// <summary>
    /// Write some debug text to the log.
    /// </summary>
    /// <param name="text">The text to be written to the log.</param>
    void Debug(string text);

    /// <summary>
    /// Write a formatted debug text to the log.
    /// </summary>
    /// <param name="format">The text to be written to the log with zero or more format items,
    /// which correspond to objects in the args array</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    void Debug(string format, params object?[] args);

    /// <summary>
    /// Write some error text to the log.
    /// </summary>
    /// <param name="text">The text to be written to the log.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Error(string text);

    /// <summary>
    /// Write an Exception to the log.
    /// </summary>
    /// <param name="exception">The exception to be written to the log.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Error(Exception exception);

    /// <summary>
    /// Write an Exception to the log.
    /// </summary>
    /// <param name="exception">The exception to be written to the log.</param>
    /// <param name="message">The message to be written to the log with the exception.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Error(Exception exception, string message);

    /// <summary>
    /// Write a formatted error text to the log.
    /// </summary>
    /// <param name="format">The text to be written to the log with zero or more format items, 
    /// which correspond to objects in the args array</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Error(string format, params object?[] args);
}
