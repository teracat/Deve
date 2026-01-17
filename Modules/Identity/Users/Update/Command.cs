namespace Deve.Identity.Users.Update;

internal sealed record Command(Guid Id, string Name, string Username, UserStatus Status, Role Role, string? Email, DateTime? Birthday) : IRequest<Result>;
