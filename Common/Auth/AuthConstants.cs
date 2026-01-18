namespace Deve.Auth;

/// <summary>
/// Provides authentication-related constants.
/// </summary>
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
}