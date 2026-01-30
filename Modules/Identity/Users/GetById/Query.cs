namespace Deve.Identity.Users.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<UserResponse>>;
