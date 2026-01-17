using System.Globalization;
using Deve.Dto.Responses.Results;
using Deve.Resources;

namespace Deve.Localize;

/// <summary>
/// Provides localization for error messages based on the specified language code.
/// </summary>
internal class ErrorLocalize : IErrorLocalize
{
    /// <summary>
    /// Stores cached <see cref="CultureInfo"/> instances for language codes.
    /// </summary>
    private readonly Dictionary<string, CultureInfo> _cultureInfos = [];

    /// <summary>
    /// Retrieves a localized error message for the specified error type and language code.
    /// </summary>
    /// <param name="errorType">The error type to localize.</param>
    /// <param name="langCode">The language code for localization.</param>
    /// <returns>
    /// The localized error message if found; otherwise, the error type name as a fallback.
    /// </returns>
    public string Localize(ResultErrorType errorType, string langCode)
    {
        CultureInfo? cultureInfo = GetCulture(langCode);
        return ErrorTypesResource.ResourceManager.GetString(errorType.ToString(), cultureInfo) ?? errorType.ToString();
    }

    /// <summary>
    /// Retrieves a <see cref="CultureInfo"/> object that corresponds to the specified language code.
    /// </summary>
    /// <remarks>If the specified language code is not found in the internal cache, a new <see
    /// cref="CultureInfo"/> is created and cached for future requests. This method never returns <c>null</c>.</remarks>
    /// <param name="langCode">The language code to retrieve the culture for. Must be a non-empty string with at least two characters. If the
    /// code is invalid, the invariant culture is returned.</param>
    /// <returns>A <see cref="CultureInfo"/> instance matching the specified language code, or <see
    /// cref="CultureInfo.InvariantCulture"/> if the code is invalid.</returns>
    public CultureInfo GetCulture(string langCode)
    {
        CultureInfo? cultureInfo = null;
        if (!string.IsNullOrWhiteSpace(langCode) && langCode.Length > 1)
        {
            _ = _cultureInfos.TryGetValue(langCode, out cultureInfo);
        }
        if (cultureInfo is null)
        {
            try
            {
                cultureInfo = new CultureInfo(langCode);
                _cultureInfos.Add(langCode, cultureInfo);
            }
            catch
            {
                // If the langCode is invalid, use the default culture
                cultureInfo = CultureInfo.InvariantCulture;
            }
        }
        return cultureInfo;
    }
}
