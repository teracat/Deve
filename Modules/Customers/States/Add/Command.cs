namespace Deve.Customers.States.Add;

internal sealed record Command(string Name, Guid CountryId) : IRequest<ResultGet<ResponseId>>;
