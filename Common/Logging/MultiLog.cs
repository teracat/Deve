namespace Deve.Logging;

/// <summary>
/// Helper class to write some text to the log.
/// </summary>
public sealed class MultiLog : ILog
{
    #region Properties
    /// <summary>
    /// List of providers where the logs will be written.
    /// </summary>
    private readonly List<ILog> _logs = [];
    #endregion

    #region Constructors
    public MultiLog() { }

    public MultiLog(params ILog[] logs)
    {
        _logs.AddRange(logs);
    }
    #endregion

    #region Methods
    /// <summary>
    /// A new provider will be used to write the logs.
    /// </summary>
    /// <param name="log">The new provider to be included. If the class type is already in the list of providers, it won't be added again.</param>
    public void Add(ILog log)
    {
        lock (_logs)
        {
            if (_logs.Any(x => x.GetType() == log.GetType()))
            {
                return;
            }

            _logs.Add(log);
        }
    }

    /// <summary>
    /// Remove a provider to stop sending messages to it.
    /// </summary>
    /// <param name="log">The provider to be removed.</param>
    /// <returns>True if the provider was removed.</returns>
    public bool Remove(ILog log)
    {
        lock (_logs)
        {
            return _logs.Remove(log);
        }
    }

    /// <summary>
    /// Remove all the providers.
    /// </summary>
    public void Clear()
    {
        lock (_logs)
        {
            _logs.Clear();
        }
    }
    #endregion

    #region ILog
    /// <summary>
    /// Write some debug text to all the log providers registered.
    /// </summary>
    /// <param name="text">The text to be written to the log.</param>
    public void Debug(string text) => _logs.ForEach(p => p.Debug(text));

    /// <summary>
    /// Write a formatted debug text to the log.
    /// </summary>
    /// <param name="format">The text to be written to the log with zero or more format items,
    /// which correspond to objects in the args array</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    public void Debug(string format, params object?[] args) => _logs.ForEach(p => p.Debug(format, args));

    /// <summary>
    /// Write some error text to all the log providers registered.
    /// </summary>
    /// <param name="text">The text to be written to the log.</param>
    public void Error(string text) => _logs.ForEach(p => p.Error(text));

    /// <summary>
    /// Write some exception to all the log providers registered.
    /// The Inner Exceptions will be concatenatedt to the end.
    /// </summary>
    /// <param name="exception">The exception to be written to the log.</param>
    public void Error(Exception exception) => _logs.ForEach(p => p.Error(exception));

    /// <summary>
    /// Write an Exception to the log.
    /// </summary>
    /// <param name="exception">The exception to be written to the log.</param>
    /// <param name="message">The message to be written to the log with the exception.</param>
    public void Error(Exception exception, string message) => _logs.ForEach(p => p.Error(exception, message));

    /// <summary>
    /// Write a formatted error text to the log.
    /// </summary>
    /// <param name="format">The text to be written to the log with zero or more format items, 
    /// which correspond to objects in the args array</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    public void Error(string format, params object?[] args) => _logs.ForEach(p => p.Error(format, args));
    #endregion
}
