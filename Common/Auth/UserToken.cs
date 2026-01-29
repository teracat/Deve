namespace Deve.Auth;

public sealed record UserToken
{
    public string Token { get; init; }
    public string Scheme { get; init; }
    public DateTimeOffset Expires { get; init; }
    public DateTimeOffset Created { get; init; }

    public UserToken()
    {
        Token = string.Empty;
        Scheme = string.Empty;
        Expires = DateTimeOffset.MinValue;
        Created = DateTimeOffset.Now;
    }

    public UserToken(string token, DateTimeOffset expires) : this(token, AuthConstants.DefaultScheme, expires, DateTimeOffset.Now) { }

    public UserToken(string token, string scheme, DateTimeOffset expires) : this(token, scheme, expires, DateTimeOffset.Now) { }

    public UserToken(string token, string scheme, DateTimeOffset expires, DateTimeOffset created)
    {
        Token = token;
        Scheme = scheme;
        Expires = expires;
        Created = created;
    }
}
