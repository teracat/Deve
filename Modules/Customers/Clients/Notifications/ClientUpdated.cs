using Deve.Abstractions.Publishers;

namespace Deve.Customers.Clients.Notifications;

public sealed record ClientUpdated(Guid ClientId) : INotification;
