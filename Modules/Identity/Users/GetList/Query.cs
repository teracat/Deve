namespace Deve.Identity.Users.GetList;

internal sealed record Query(Guid? Id, string? Name, string? Username, string? PasswordHash, UserActiveType? UserActiveType, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<UserResponse>>;
