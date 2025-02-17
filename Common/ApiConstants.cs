﻿namespace Deve
{
    public static class ApiConstants
    {
        #region Url External
//-:cnd
#if DEBUG
        public static readonly string UrlProductionExternal   = "https://localhost:7175";
        public static readonly string UrlStagingExternal      = "https://localhost:7175";
#else
        //TODO: change with real URLs
        public static readonly string UrlProductionExternal   = "https://api.deve.com/v1";
        public static readonly string UrlStagingExternal      = "https://dev.api.deve.com/v1";
#endif
//+:cnd
        #endregion

        #region Auth
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
