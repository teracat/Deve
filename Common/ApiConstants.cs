namespace Deve
{
    /// <summary>
    /// Defines API-related constant values, including external URLs, authentication settings, paths, and method names.
    /// </summary>
    public static class ApiConstants
    {
        #region Url External
        /// <summary>
        /// The external production API URL.
        /// </summary>
//-:cnd
#if DEBUG
        public static readonly string UrlProductionExternal   = "https://localhost:7175";
#else
        //TODO: change with real URLs
        public static readonly string UrlProductionExternal   = "https://api.deve.com/v1";
#endif
//+:cnd

        /// <summary>
        /// The external staging API URL.
        /// </summary>
//-:cnd
#if DEBUG
        public static readonly string UrlStagingExternal      = "https://localhost:7175";
#else
        //TODO: change with real URLs
        public static readonly string UrlStagingExternal      = "https://dev.api.deve.com/v1";
#endif
//+:cnd
        #endregion

        #region Auth
        /// <summary>
        /// The default authentication scheme used for API requests.
        /// </summary>
        public static readonly string AuthDefaultScheme    = "Bearer";
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
