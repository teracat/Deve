namespace Deve.Sdk;

internal static class SdkConstants
{
#pragma warning disable S1075 // URIs should not be hardcoded
//-:cnd
#if DEBUG
    public static readonly Uri UrlProduction = new("https://localhost:7245");
    public static readonly Uri UrlStaging = new("https://localhost:7245");
#else
    //TODO: change with real URLs
    public static readonly Uri UrlProduction = new("https://api-int.deve.com/");
    public static readonly Uri UrlStaging = new("https://dev.api-int.deve.com/");
#endif
//+:cnd
#pragma warning restore S1075 // URIs should not be hardcoded
}
