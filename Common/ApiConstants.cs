namespace Deve;

/// <summary>
/// Defines API-related constant values, including authentication settings, paths, and method names.
/// </summary>
public static class ApiConstants
{
    #region Paths
    public const string BasePath = "/";
    public const string BasePathV1 = BasePath + "v1/";

    public const string PathIdentityV1 = BasePathV1 + "Identity/";
    public const string PathUserV1 = PathIdentityV1 + "User/";

    public const string PathAuthV1 = BasePathV1 + "Auth/";

    public const string PathCustomersV1 = BasePathV1 + "Customers/";

    public const string PathCountryV1 = PathCustomersV1 + "Country/";
    public const string PathStateV1 = PathCustomersV1 + "State/";
    public const string PathCityV1 = PathCustomersV1 + "City/";
    public const string PathClientV1 = PathCustomersV1 + "Client/";
    public const string PathStatsV1 = PathCustomersV1 + "Stats/";
    #endregion

    #region Methods
    public const string MethodSeparator = "/";
    public const string MethodLogin = "Login";
    public const string MethodRefreshToken = "RefreshToken";
    public const string MethodGetClientStats = "GetClientStats";
    public const string MethodUpdateStatus = "UpdateStatus";
    public const string MethodPassword = "Password";
    public const string MethodGetByUsernamePassword = "GetByUsernamePassword";
    #endregion

    #region Tags
    public const string TagAuth = "Auth";
    public const string TagIdentityUser = "User";
    public const string TagCustomersCountry = "Country";
    public const string TagCustomersState = "State";
    public const string TagCustomersCity = "City";
    public const string TagCustomersClient = "Client";
    public const string TagCustomersStats = "Stats";
    #endregion
}
