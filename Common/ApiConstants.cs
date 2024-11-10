namespace Deve
{
    public static class ApiConstants
    {
        #region Url External
#if DEBUG
        public const string UrlProductionExternal   = "http://localhost:5057";
        public const string UrlStagingExternal      = "http://localhost:5057";
#else
        //TODO: change with real URLs
        public const string UrlProductionExternal   = "http://api.deve.com/v1";
        public const string UrlStagingExternal      = "http://dev.api.deve.com/v1";
#endif
        #endregion

        #region Auth
        public const string ApiAuthDefaultScheme    = "Bearer";
        public const string ApiAuthCryptAesScheme   = "Custom"; //Custom implementation using AES algorithm
        #endregion

        #region Paths
        public const string ApiBasePath             = "/";

        public const string ApiPathAuth             = ApiBasePath + "Auth/";
        public const string ApiPathCountry          = ApiBasePath + "Country/";
        public const string ApiPathState            = ApiBasePath + "State/";
        public const string ApiPathCity             = ApiBasePath + "City/";
        public const string ApiPathClient           = ApiBasePath + "Client/";
        public const string ApiPathClientBasic      = ApiBasePath + "ClientBasic/";
        public const string ApiPathUser             = ApiBasePath + "User/";
        public const string ApiPathStats            = ApiBasePath + "Stats/";
        #endregion

        #region Methods
        public const string ApiMethodLogin          = "Login";
        public const string ApiMethodRefreshToken   = "RefreshToken";
        public const string ApiMethodGetClientStats = "GetClientStats";
        public const string ApiMethodUpdateStatus   = "UpdateStatus";
        #endregion
    }
}
