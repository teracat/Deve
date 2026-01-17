namespace Deve.Auth.Token;

/// <summary>
/// Information that includes the token (used in TokenManagerCrypt).
/// </summary>
public sealed record TokenData(UserIdentity Subject, DateTimeOffset Expires) : ITokenData
{
    public static TokenData Create(UserIdentity subject) => new(subject, DateTimeOffset.UtcNow.AddHours(AuthConstants.TokenExpiresInHours));
}
