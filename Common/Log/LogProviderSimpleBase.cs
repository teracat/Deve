namespace Deve
{
    internal abstract class LogProviderSimpleBase : ILogProvider
    {
        #region Constants
        /// <summary>
        /// Name of the Date variable that can be used in the DebugStringFormat and ErrorStringFormat.
        /// This will be replaced by the current date & time when any text is written to the log.
        /// </summary>
        public const string Date = "{Date}";
        /// <summary>
        /// Name of the Text variable that can be used in the DebugStringFormat and ErrorStringFormat.
        /// This will be replaced by the text you send to the log.
        /// </summary>
        public const string Text = "{Text}";
        #endregion

        #region Properties
        /// <summary>
        /// Format to be used when the date & time is written to the log.
        /// </summary>
        public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
        /// <summary>
        /// Format to be used when the text is written to the log using the Debug method.
        /// </summary>
        public string DebugStringFormat { get; set; } = $"{Date} DEBUG - {Text}";
        /// <summary>
        /// Format to be used when the text is written to the log using the Error method.
        /// </summary>
        public string ErrorStringFormat { get; set; } = $"{Date} ERROR - {Text}";
        #endregion

        #region Constructor
        /// <summary>
        /// Used to write log messages to the console.
        /// </summary>
        /// <param name="dateFormat">Format to be used when the date & time is written to the log. If null, the default format will be used.</param>
        /// <param name="debugStringFormat">Format to be used when the text is written to the log using the Debug method. If null, the default format will be used.</param>
        /// <param name="errorStringFormat">Format to be used when the text is written to the log using the Error method. If null, the default format will be used.</param>
        public LogProviderSimpleBase(string? dateFormat = null, string? debugStringFormat = null, string? errorStringFormat = null)
        {
            if (dateFormat is not null)
                DateFormat = dateFormat;
            if (debugStringFormat is not null)
                DebugStringFormat = debugStringFormat;
            if (errorStringFormat is not null)
                ErrorStringFormat = errorStringFormat;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Use the specified format and text to build the message that will be written to the console.
        /// </summary>
        /// <param name="format">Format to be used.</param>
        /// <param name="text">Text to include.</param>
        /// <returns></returns>
        private string FormatText(string format, string text)
        {
            return format.Replace(Date, DateTime.Now.ToString(DateFormat))
                         .Replace(Text, text);
        }

        protected abstract void Write(string text);
        #endregion

        #region ILogProvider
        /// <summary>
        /// Write some debug text to the log.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        public void Debug(string text)
        {
            Write(FormatText(DebugStringFormat, text));
        }

        /// <summary>
        /// Write some error text to the log.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        public void Error(string text)
        {
            Write(FormatText(ErrorStringFormat, text));
        }
        #endregion
    }
}
