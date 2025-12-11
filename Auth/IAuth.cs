using Deve.Auth.Hash;
using Deve.Auth.Permissions;
using Deve.Auth.TokenManagers;
using Deve.Authenticate;
using Deve.Data;
using Deve.Internal.Dto;
using Deve.Dto;

namespace Deve.Auth
{
    /// <summary>
    /// Provides additional authentication functionality and permission checks.
    /// </summary>
    public interface IAuth : IAuthenticate, IDisposable
    {
        IDataOptions Options { get; set; }
        ITokenManager TokenManager { get; }
        IHash Hash { get; }

        /// <summary>
        /// Authenticates a user based on the provided credentials.
        /// </summary>
        /// <param name="userCredentials">The user's login credentials.</param>
        /// <returns>
        /// A <see cref="User"/> containing the authenticated user if successful,
        /// or an error result if authentication fails.
        /// </returns>
        Task<ResultGet<User>> LoginUser(UserCredentials userCredentials);

        /// <summary>
        /// Checks whether the specified permission is granted for the given user and data type.
        /// </summary>
        /// <param name="user">The user for whom the permission is checked. If null, public access is determined.</param>
        /// <param name="type">The type of permission to check.</param>
        /// <param name="dataType">The data type associated with the permission check.</param>
        /// <returns>
        /// A <see cref="PermissionResult"/> indicating whether the permission is granted.
        /// </returns>
        Task<PermissionResult> IsGranted(IUserIdentity? user, PermissionType type, PermissionDataType dataType);
    }
}
