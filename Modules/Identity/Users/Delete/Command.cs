namespace Deve.Identity.Users.Delete;

internal sealed record Command(Guid Id) : IRequest<Result>;
