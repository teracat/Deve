using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserGetByUsernamePasswordRequest
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public UserActiveType ActiveType { get; init; } = UserActiveType.All;
}
