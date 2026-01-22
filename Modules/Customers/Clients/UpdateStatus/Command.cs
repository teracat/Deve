using Deve.Customers.Enums;

namespace Deve.Customers.Clients.UpdateStatus;

internal sealed record Command(Guid Id, ClientStatus Status) : IRequest<Result>;
