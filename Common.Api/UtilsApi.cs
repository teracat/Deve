using Microsoft.AspNetCore.Http;

namespace Deve.Api
{
    /// <summary>
    /// Helper methods to be used in the API projects.
    /// </summary>
    public static class UtilsApi
    {
        /// <summary>
        /// Get the preferred language from the AcceptLanguage header in the HttpResquest.
        /// </summary>
        /// <param name="request">Request to check the AcceptLanguage header.</param>
        /// <returns>Language Code to be used.</returns>
        public static string? GetLangCodeFromRequest(HttpRequest? request)
        {
            if (request is null)
            {
                return null;
            }

            string? langCode;
            // Quality defines priority from 0 to 1, where 1 is the highest.
            var languages = request.GetTypedHeaders()
                                   .AcceptLanguage
                                   .OrderByDescending(x => x.Quality ?? 1)
                                   .Select(x => x.Value.ToString())
                                   .ToList();
            if (languages.Count == 1)
            {
                langCode = languages[0];
            }
            else
            {
                //First look for some of the available languages
                langCode = languages.FirstOrDefault(x => Constants.AvailableLanguages.Contains(x));

                //If none found, we'll use the first language received
                if (string.IsNullOrEmpty(langCode))
                {
                    langCode = languages.FirstOrDefault();
                }
            }
            return langCode;
        }
    }
}
