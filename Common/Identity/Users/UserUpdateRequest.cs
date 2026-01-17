using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserUpdateRequest(string Name, string Username, UserStatus Status, Role Role, string? Email, DateTime? Birthday);
