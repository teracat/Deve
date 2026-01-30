namespace Deve.Customers.States.Delete;

internal sealed record Command(Guid Id) : IRequest<Result>;
