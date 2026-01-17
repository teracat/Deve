namespace Deve.Customers.Countries.Update;

internal sealed record Command(Guid Id, string Name, string IsoCode) : IRequest<Result>;
