namespace Deve.Customers.Cities.Delete;

internal sealed record Command(Guid Id) : IRequest<Result>;
