namespace Deve;

/// <summary>
/// Defines application-wide constant values.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Root path used as the base for API.
    /// </summary>
    public static readonly string BasePathApi = "/";

    /// <summary>
    /// Base URL path for version 1 of the API.
    /// </summary>
    public static readonly string BasePathApiV1 = BasePathApi + "v1/";

    /// <summary>
    /// Character used to separate method names in URLs.
    /// </summary>
    public static readonly string MethodSeparator = "/";

    /// <summary>
    /// The default limit for queries.
    /// </summary>
    public static readonly int DefaultLimit = 50;

    /// <summary>
    /// The language code for English.
    /// </summary>
    public static readonly string LanguageCodeEnglish = "en";

    /// <summary>
    /// The language code for Spanish.
    /// </summary>
    public static readonly string LanguageCodeSpanish = "es";

    /// <summary>
    /// The language code for Catalan.
    /// </summary>
    public static readonly string LanguageCodeCatalan = "ca";

    /// <summary>
    /// The default language code used by the application.
    /// </summary>
    public static readonly string DefaultLangCode = LanguageCodeEnglish;

    /// <summary>
    /// The list of available languages supported by the application.
    /// </summary>
    public static readonly string[] AvailableLanguages =
    [
        LanguageCodeEnglish,
        LanguageCodeSpanish,
        LanguageCodeCatalan
    ];
}
