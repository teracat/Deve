namespace Deve.Customers.Countries.Delete;

internal sealed record Command(Guid Id) : IRequest<Result>;
