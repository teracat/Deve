namespace Deve.Customers.Clients.Delete;

internal sealed record Command(Guid Id) : IRequest<Result>;
