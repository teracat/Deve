namespace Deve
{
    internal class LogProviderDebug : LogProviderSimpleBase, ILogProvider
    {
        #region Constructor
        /// <summary>
        /// Used to write log messages to the Debug.
        /// </summary>
        /// <param name="dateFormat">Format to be used when the date & time is written to the log. If null, the default format will be used.</param>
        /// <param name="debugStringFormat">Format to be used when the text is written to the log using the Debug method. If null, the default format will be used.</param>
        /// <param name="errorStringFormat">Format to be used when the text is written to the log using the Error method. If null, the default format will be used.</param>
        public LogProviderDebug(string? dateFormat = null, string? debugStringFormat = null, string? errorStringFormat = null)
            : base(dateFormat, debugStringFormat, errorStringFormat)
        {
        }
        #endregion

        #region ILogProvider
        /// <summary>
        /// Write the text to the Debug.
        /// </summary>
        /// <param name="text">The text to be written.</param>
        protected override void Write(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }
        #endregion
    }

    public static class LogProviderDebugExtension
    {
        private static LogProviderDebug? _instance;

        public static void AddDebug(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderDebug();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveDebug(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
