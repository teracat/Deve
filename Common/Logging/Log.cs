namespace Deve.Logging
{
    /// <summary>
    /// Helper class to write some text to the log.
    /// </summary>
    public static class Log
    {
        #region Properties
        /// <summary>
        /// List of providers where the logs will be written.
        /// </summary>
        public static LogProviders Providers { get; } = new LogProviders();
        #endregion

        #region Methods
        /// <summary>
        /// Write some debug text to all the log providers registered.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        public static void Debug(string text) => Providers.List.ForEach(p => p.Debug(text));

        /// <summary>
        /// Write a formatted debug text to the log.
        /// </summary>
        /// <param name="format">The text to be written to the log with zero or more format items,
        /// which correspond to objects in the args array</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug(string format, params object?[] args) => Providers.List.ForEach(p => p.Debug(format, args));

        /// <summary>
        /// Write some error text to all the log providers registered.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        public static void Error(string text) => Providers.List.ForEach(p => p.Error(text));

        /// <summary>
        /// Write some exception to all the log providers registered.
        /// The Inner Exceptions will be concatenatedt to the end.
        /// </summary>
        /// <param name="exception">The exception to be written to the log.</param>
        public static void Error(Exception exception) => Providers.List.ForEach(p => p.Error(exception));

        /// <summary>
        /// Write an Exception to the log.
        /// </summary>
        /// <param name="exception">The exception to be written to the log.</param>
        /// <param name="message">The message to be written to the log with the exception.</param>
        public static void Error(Exception exception, string message) => Providers.List.ForEach(p => p.Error(exception, message));

        /// <summary>
        /// Write a formatted error text to the log.
        /// </summary>
        /// <param name="format">The text to be written to the log with zero or more format items, 
        /// which correspond to objects in the args array</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error(string format, params object?[] args) => Providers.List.ForEach(p => p.Error(format, args));
        #endregion
    }
}
