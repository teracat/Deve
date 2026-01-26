namespace Deve.Auth;

public static class AuthConstants
{
    /// <summary>
    /// The default authentication scheme used for API requests.
    /// </summary>
    public static readonly string DefaultScheme = "Bearer";

    /// <summary>
    /// The number of hours before an authentication token expires.
    /// </summary>
    public static readonly int TokenExpiresInHours = 24;

    /// <summary>
    /// The claim key for the user's unique identifier.
    /// </summary>
    public static readonly string UserClaimId = "Id";

    /// <summary>
    /// The claim key for the user's username.
    /// </summary>
    public static readonly string UserClaimUsername = "Username";

    /// <summary>
    /// The claim key for the user's role.
    /// </summary>
    public static readonly string UserClaimRole = "Role";

    public static readonly string PathAuthV1 = Constants.BasePathApiV1 + "Auth/";

    // <hooks:constants-path>

    // <hooks:constants-method>
    public static readonly string MethodLogin = "Login";
    public static readonly string MethodRefreshToken = "RefreshToken";

    // <hooks:constants-tag>
    public static readonly string TagAuth = "Auth";
}