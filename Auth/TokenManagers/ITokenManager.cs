using Deve.Authenticate;
using Deve.Internal.Model;

namespace Deve.Auth.TokenManagers
{
    public interface ITokenManager
    {
        UserToken CreateToken(User user, string scheme = ApiConstants.AuthDefaultScheme);
        TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity);
    }
}
