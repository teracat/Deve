using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserGetByUsernamePasswordRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required UserActiveType ActiveType { get; init; }
}
