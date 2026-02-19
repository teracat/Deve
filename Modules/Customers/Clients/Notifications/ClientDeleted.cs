using Deve.Abstractions.Publishers;

namespace Deve.Customers.Clients.Notifications;

public sealed record ClientDeleted(Guid ClientId) : INotification;
