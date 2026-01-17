using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserAddRequest(string Name, string Username, string Password, UserStatus Status, Role Role, string? Email, DateTime? Birthday);
