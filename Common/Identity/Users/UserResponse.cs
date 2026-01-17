using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserResponse(Guid Id, string Name, string Username, UserStatus Status, Role Role, string? Email, DateTime? Birthday, DateTimeOffset Joined);
