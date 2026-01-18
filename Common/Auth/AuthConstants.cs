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
    public const string UserClaimId = "Id";

    /// <summary>
    /// The claim key for the user's username.
    /// </summary>
    public const string UserClaimUsername = "Username";

    /// <summary>
    /// The claim key for the user's role.
    /// </summary>
    public const string UserClaimRole = "Role";

    public const string PathAuthV1 = ApiConstants.BasePathV1 + "Auth/";

    public const string MethodLogin = "Login";
    public const string MethodRefreshToken = "RefreshToken";

    public const string TagAuth = "Auth";
}