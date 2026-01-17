namespace Deve.Customers.States.Update;

internal sealed record Command(Guid Id, string Name, Guid CountryId) : IRequest<Result>;
