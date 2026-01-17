using System.Globalization;
using Deve.Dto.Responses.Results;

namespace Deve.Localize;

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

    /// <summary>
    /// Retrieves a CultureInfo object that corresponds to the specified language code.
    /// </summary>
    /// <remarks>If the specified language code is not recognized, the method may return a default or
    /// invariant culture. This method is useful for localizing content or formatting data according to user preferences.</remarks>
    /// <param name="langCode">The language code to use for retrieving culture information. Must be a valid IETF language tag, such as "en-US"
    /// or "fr-FR".</param>
    /// <returns>A CultureInfo instance representing the culture associated with the specified language code.</returns>
    CultureInfo GetCulture(string langCode);
}
