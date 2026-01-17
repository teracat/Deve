namespace Deve.Customers.Countries.Add;

internal sealed record Command(string Name, string IsoCode) : IRequest<ResultGet<ResponseId>>;
