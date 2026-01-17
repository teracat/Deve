namespace Deve.Customers.Cities.Add;

internal sealed record Command(string Name, Guid StateId) : IRequest<ResultGet<ResponseId>>;
