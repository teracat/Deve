namespace Deve.Customers.Clients.Notifications;

internal class ClientUpdatedDeletedHandler(ICache cache) : INotificationHandler<ClientUpdated>, INotificationHandler<ClientDeleted>
{
    public Task HandleAsync(ClientUpdated notification, CancellationToken cancellationToken = default) => HandleAsync(notification.ClientId, cancellationToken);

    public Task HandleAsync(ClientDeleted notification, CancellationToken cancellationToken = default) => HandleAsync(notification.ClientId, cancellationToken);

    private Task HandleAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var cacheKey = Utils.GetCacheKeyForType<ClientResponse>(id);
            cache.Remove(cacheKey);
        }, cancellationToken);
}
