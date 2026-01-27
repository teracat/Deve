namespace Deve.Customers.Clients.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<ClientResponse>>;
