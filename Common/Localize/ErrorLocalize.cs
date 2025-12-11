using System.Globalization;
using Deve.Dto;
using Deve.Resources;

namespace Deve.Localize
{
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
            return ErrorTypesResource.ResourceManager.GetString(errorType.ToString(), cultureInfo) ?? errorType.ToString();
        }
    }
}