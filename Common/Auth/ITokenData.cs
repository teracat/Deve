namespace Deve.Auth;

public interface ITokenData
{
    UserIdentity Subject { get; }

    DateTimeOffset Expires { get; }
}
