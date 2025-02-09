namespace Deve.Internal
{
    public static class ApiInternalConstants
    {
//-:cnd
#if DEBUG
        public const string UrlProduction   = "https://af83-67-218-252-62.ngrok-free.app";
        public const string UrlStaging      = "https://af83-67-218-252-62.ngrok-free.app";
#else
        //TODO: change with real URLs
        public const string UrlProduction   = "https://api-int.deve.com/v1";
        public const string UrlStaging      = "https://dev.api-int.deve.com/v1";
#endif
//+:cnd
    }
}
