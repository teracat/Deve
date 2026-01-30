namespace Deve.Identity.Users.GetByUsernamePassword;

internal sealed record Query(string Username, string Password, UserActiveType ActiveType) : IRequest<ResultGet<UserResponse>>;
