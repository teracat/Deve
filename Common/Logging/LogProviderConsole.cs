namespace Deve.Logging
{
    /// <summary>
    /// A logging provider that outputs log messages to the console.
    /// </summary>
    internal class LogProviderConsole : LogProviderSimpleBase, ILogProvider
    {
        #region Constructor
        /// <summary>
        /// Used to write log messages to the Console.
        /// </summary>
        /// <param name="dateFormat">Format to be used when the date & time is written to the log. If null, the default format will be used.</param>
        /// <param name="debugStringFormat">Format to be used when the text is written to the log using the Debug method. If null, the default format will be used.</param>
        /// <param name="errorStringFormat">Format to be used when the text is written to the log using the Error method. If null, the default format will be used.</param>
        public LogProviderConsole(string? dateFormat = null, string? debugStringFormat = null, string? errorStringFormat = null)
            : base(dateFormat, debugStringFormat, errorStringFormat)
        {
        }
        #endregion

        #region ILogProvider
        /// <summary>
        /// Write the text to the Console.
        /// </summary>
        /// <param name="text">The text to be written.</param>
        protected override void Write(string text)
        {
            Console.WriteLine(text);
        }
        #endregion
    }

    public static class LogProviderConsoleExtension
    {
        private static LogProviderConsole? _instance;

        public static void AddConsole(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderConsole();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveConsole(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
