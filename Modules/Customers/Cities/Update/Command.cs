namespace Deve.Customers.Cities.Update;

internal sealed record Command(Guid Id, string Name, Guid StateId) : IRequest<Result>;
