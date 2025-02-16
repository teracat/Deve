using Deve.Authenticate;
using Deve.Internal.Model;

namespace Deve.Auth.TokenManagers
{
    public interface ITokenManager : IDisposable
    {
        UserToken CreateToken(User user, string scheme);

        UserToken CreateToken(User user);

        bool TryValidateToken(string token, out UserIdentity? userIdentity);
    }
}