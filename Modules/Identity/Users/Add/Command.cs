namespace Deve.Identity.Users.Add;

internal sealed record Command(string Name, string Username, string Password, UserStatus Status, Role Role, string? Email, DateTime? Birthday) : IRequest<ResultGet<ResponseId>>;
