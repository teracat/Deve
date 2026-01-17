using Deve.Customers.Enums;

namespace Deve.Customers.Clients.UpdateStatus;

internal sealed record UpdateClientStatusCommand(Guid Id, ClientStatus Status) : IRequest<Result>;
