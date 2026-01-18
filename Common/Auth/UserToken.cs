namespace Deve.Auth;

public sealed record UserToken(string Token, string Scheme, DateTimeOffset Expires, DateTimeOffset Created)
{
    public static UserToken Create(string token, DateTimeOffset expires) => new(token, AuthConstants.DefaultScheme, expires, DateTimeOffset.Now);

    public static UserToken Create(string token, string scheme, DateTimeOffset expires) => new(token, scheme, expires, DateTimeOffset.Now);
}
