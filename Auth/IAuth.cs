namespace Deve.Auth
{
    public interface IAuth : IAuthenticate
    {
        DataOptions Options { get; set; }
        ITokenManager TokenManager { get; }
        IHash Hash { get; }
        ICrypt Crypt { get; }

        Task<ResultGet<User>> LoginUser(UserCredentials userCredentials);
        Task<PermissionResult> IsGranted(UserIdentity? user, PermissionType type, PermissionDataType dataType);
    }
}
