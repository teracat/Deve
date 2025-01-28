namespace Deve
{
    public static class ApiConstants
    {
        #region Url External
//-:cnd
#if DEBUG
        public const string UrlProductionExternal   = "http://localhost:5057";
        public const string UrlStagingExternal      = "http://localhost:5057";
#else
        //TODO: change with real URLs
        public const string UrlProductionExternal   = "http://api.deve.com/v1";
        public const string UrlStagingExternal      = "http://dev.api.deve.com/v1";
#endif
//+:cnd
        #endregion

        #region Auth
        public const string AuthDefaultScheme    = "Bearer";
        #endregion

        #region Paths
        public const string BasePath             = "/";

        public const string PathAuth             = BasePath + "Auth/";
        public const string PathCountry          = BasePath + "Country/";
        public const string PathState            = BasePath + "State/";
        public const string PathCity             = BasePath + "City/";
        public const string PathClient           = BasePath + "Client/";
        public const string PathClientBasic      = BasePath + "ClientBasic/";
        public const string PathUser             = BasePath + "User/";
        public const string PathStats            = BasePath + "Stats/";
        #endregion

        #region Methods
        public const string MethodLogin          = "Login";
        public const string MethodRefreshToken   = "RefreshToken";
        public const string MethodGetClientStats = "GetClientStats";
        public const string MethodUpdateStatus   = "UpdateStatus";
        #endregion
    }
}
