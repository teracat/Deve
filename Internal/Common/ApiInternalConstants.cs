namespace Deve.Internal
{
    public static class ApiInternalConstants
    {
//-:cnd
#if DEBUG
        public const string UrlProduction   = "https://localhost:7245";
        public const string UrlStaging      = "https://localhost:7245";
#else
        //TODO: change with real URLs
        public const string UrlProduction   = "https://api-int.deve.com/v1";
        public const string UrlStaging      = "https://dev.api-int.deve.com/v1";
#endif
//+:cnd
    }
}
