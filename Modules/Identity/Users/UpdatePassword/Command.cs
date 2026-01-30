namespace Deve.Identity.Users.UpdatePassword;

internal sealed record Command(Guid Id, string Password) : IRequest<Result>;
