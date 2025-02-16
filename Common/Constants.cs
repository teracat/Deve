namespace Deve
{
    public static class Constants
    {
        public static readonly int DefaultCriteriaLimit               = 50;

        //Languages
        public static readonly string LanguageCodeEnglish             = "en";
        public static readonly string LanguageCodeSpanish             = "es";
        public static readonly string LanguageCodeCatalan             = "ca";
        public static readonly string DefaultLangCode                 = LanguageCodeEnglish;
        public static readonly string[] AvailableLanguages  = [LanguageCodeEnglish, LanguageCodeSpanish, LanguageCodeCatalan];
    }
}