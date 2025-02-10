using Deve.Model;

namespace Deve.Authenticate
{
    public interface IAuthenticate
    {
        Task<ResultGet<UserToken>> Login(UserCredentials userCredentials);
        Task<ResultGet<UserToken>> RefreshToken(string token);
    }
}
