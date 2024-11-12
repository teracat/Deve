namespace Deve.Auth
{
    public interface ITokenManager
    {
        UserToken CreateToken(User user, string scheme = ApiConstants.ApiAuthDefaultScheme);
        TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity);
    }
}
