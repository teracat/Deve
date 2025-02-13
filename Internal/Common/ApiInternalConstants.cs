namespace Deve.Internal
{
    public static class ApiInternalConstants
    {
//-:cnd
#if DEBUG
        public static readonly string UrlProduction   = "https://localhost:7245";
        public static readonly string UrlStaging      = "https://localhost:7245";
#else
        //TODO: change with real URLs
        public static readonly string UrlProduction   = "https://api-int.deve.com/v1";
        public static readonly string UrlStaging      = "https://dev.api-int.deve.com/v1";
#endif
//+:cnd
    }
}