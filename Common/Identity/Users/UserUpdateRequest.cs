using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserUpdateRequest
{
    public string Name { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public UserStatus Status { get; init; } = UserStatus.Inactive;
    public Role Role { get; init; } = Role.User;

    public string? Email { get; init; }
    public DateTime? Birthday { get; init; }
}
