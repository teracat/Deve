using System.Linq;
using System.Text;

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
        public static void Debug(string text)
        {
            Providers.List.ForEach(p => p.Debug(text));
        }

        /// <summary>
        /// Write some error text to all the log providers registered.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        public static void Error(string text)
        {
            Providers.List.ForEach(p => p.Error(text));
        }

        /// <summary>
        /// Write some exception to all the log providers registered.
        /// The Inner Exceptions will be concatenatedt to the end.
        /// </summary>
        /// <param name="exception">The exception to be written to the log.</param>
        public static void Error(Exception exception)
        {
            var text = new StringBuilder(exception.Message);
            Exception? innerEx = exception.InnerException;
            while (innerEx is not null)
            {
                text.Append(" | ").Append(innerEx.Message);
                innerEx = innerEx.InnerException;
            }
            Error(text.ToString());

        }
        #endregion
    }
}