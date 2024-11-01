namespace Deve
{
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
        /// Write some error text to the log.
        /// </summary>
        /// <param name="text">The text to be written to the log.</param>
        void Error(string text);
    }
}
