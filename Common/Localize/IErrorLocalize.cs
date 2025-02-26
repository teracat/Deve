using Deve.Model;

namespace Deve.Localize
{
    /// <summary>
    /// Defines a contract for localizing error messages based on a specified language code.
    /// </summary>
    public interface IErrorLocalize
    {
        /// <summary>
        /// Retrieves a localized error message for the specified error type and language code.
        /// </summary>
        /// <param name="errorType">The error type to localize.</param>
        /// <param name="langCode">The language code for localization.</param>
        /// <returns>
        /// The localized error message if available; otherwise, the error type name as a fallback.
        /// </returns>
        string Localize(ResultErrorType errorType, string langCode);
    }

}
