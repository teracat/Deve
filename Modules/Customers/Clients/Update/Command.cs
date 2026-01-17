namespace Deve.Customers.Clients.Update;

internal sealed record Command(Guid Id, string Name, Guid CityId) : IRequest<Result>;
