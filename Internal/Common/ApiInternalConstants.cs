namespace Deve.Internal
{
    public static class ApiInternalConstants
    {
//-:cnd
#if DEBUG
        public const string UrlProduction   = "http://localhost:5053";
        public const string UrlStaging      = "http://localhost:5053";
#else
        //TODO: change with real URLs
        public const string UrlProduction   = "http://api-int.deve.com/v1";
        public const string UrlStaging      = "http://dev.api-int.deve.com/v1";
#endif
//+:cnd
    }
}
