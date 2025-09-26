using System.Diagnostics;

namespace Deve.Logging
{
    /// <summary>
    /// A logging provider that sends log messages to the trace output.
    /// </summary>
    internal class LogProviderTrace : LogProviderSimpleBase, ILogProvider
    {
        #region Constructor
        /// <summary>
        /// Used to write log messages to the Trace.
        /// </summary>
        /// <param name="dateFormat">Format to be used when the date & time is written to the log. If null, the default format will be used.</param>
        /// <param name="debugStringFormat">Format to be used when the text is written to the log using the Debug method. If null, the default format will be used.</param>
        /// <param name="errorStringFormat">Format to be used when the text is written to the log using the Error method. If null, the default format will be used.</param>
        public LogProviderTrace(string? dateFormat = null, string? debugStringFormat = null, string? errorStringFormat = null)
            : base(dateFormat, debugStringFormat, errorStringFormat)
        {
        }
        #endregion

        #region ILogProvider
        /// <summary>
        /// Write the text to the Trace.
        /// </summary>
        /// <param name="text">The text to be written.</param>
        protected override void Write(string text)
        {
            Trace.WriteLine(text);
        }
        #endregion
    }

    public static class LogProviderTraceExtension
    {
        private static LogProviderTrace? _instance;

        public static void AddTrace(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderTrace();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveTrace(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
