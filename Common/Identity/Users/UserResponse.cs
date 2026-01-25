using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required UserStatus Status { get; init; }
    public required Role Role { get; init; }
    public required string? Email { get; init; }
    public required DateTime? Birthday { get; init; }
    public required DateTimeOffset Joined { get; init; }
}
