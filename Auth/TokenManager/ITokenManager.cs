namespace Deve.Auth
{
    public interface ITokenManager
    {
        UserToken CreateToken(User user, string scheme = ApiConstants.AuthDefaultScheme);
        TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity);
    }
}
