using Deve.Authenticate;
using Deve.Data;
using Deve.Model;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Auth.Permissions;
using Deve.Internal.Model;

namespace Deve.Auth
{
    public interface IAuth : IAuthenticate, IDisposable
    {
        DataOptions Options { get; set; }
        ITokenManager TokenManager { get; }
        IHash Hash { get; }

        Task<ResultGet<User>> LoginUser(UserCredentials userCredentials);
        Task<PermissionResult> IsGranted(UserIdentity? user, PermissionType type, PermissionDataType dataType);
    }
}
