namespace Deve.Auth;

/// <summary>
/// Provides authentication-related constants.
/// </summary>
public static class AuthConstants
{
    /// <summary>
    /// The number of hours before an authentication token expires.
    /// </summary>
    public static readonly int TokenExpiresInHours = 24;

    /// <summary>
    /// The claim key for the user's unique identifier.
    /// </summary>
    internal const string UserClaimId = "Id";

    /// <summary>
    /// The claim key for the user's username.
    /// </summary>
    internal const string UserClaimUsername = "Username";

    /// <summary>
    /// The claim key for the user's role.
    /// </summary>
    internal const string UserClaimRole = "Role";
}