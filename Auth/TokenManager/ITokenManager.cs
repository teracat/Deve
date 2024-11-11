namespace Deve.Auth
{
    public interface ITokenManager
    {
        UserToken CreateToken(User user);
        TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity);
    }
}
