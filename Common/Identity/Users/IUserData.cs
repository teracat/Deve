using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Identity.Users;

public interface IUserData : IModuleItem
{
    // Queries
    Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<UserResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request, CancellationToken cancellationToken = default);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, UserUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-commands>
}
