namespace Deve.Customers.States.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<StateResponse>>;
