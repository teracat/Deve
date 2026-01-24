using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Identity;
using Deve.Identity.Users;

namespace Deve.Sdk.Identity;

internal class UserSdk : BaseSdk, IUserData
{
    private static readonly string Path = IdentityConstants.PathUserV1;

    public UserSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request, CancellationToken cancellationToken) => await GetList<UserResponse, UserGetListRequest>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<ResultGet<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await Get<UserResponse>(Path, RequestAuthType.Default, id, cancellationToken);
    public async Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request, CancellationToken cancellationToken) => await Get<UserResponse, UserGetByUsernamePasswordRequest>(Path + IdentityConstants.MethodGetByUsernamePassword, RequestAuthType.Default, request, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request, CancellationToken cancellationToken) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, UserUpdateRequest request, CancellationToken cancellationToken) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request, CancellationToken cancellationToken) => await Patch(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
