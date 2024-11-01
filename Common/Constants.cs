namespace Deve
{
    public static class Constants
    {
        public const int DefaultCriteriaLimit               = 50;

        //Languages
        public const string LanguageCodeEnglish             = "en";
        public const string LanguageCodeSpanish             = "es";
        public const string LanguageCodeCatalan             = "ca";
        public const string DefaultLangCode                 = LanguageCodeEnglish;
        public static readonly string[] AvailableLanguages  = [LanguageCodeEnglish, LanguageCodeSpanish, LanguageCodeCatalan];
    }
}
