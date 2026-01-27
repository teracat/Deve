using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Identity.Users;

public interface IUserData : IModuleItem
{
    // Queries
    Task<ResultGetList<UserResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<UserResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<UserResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request) => GetByUsernamePasswordAsync(request, CancellationToken.None);
    Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, UserUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, UserUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request) => UpdatePasswordAsync(id, request, CancellationToken.None);
    Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
