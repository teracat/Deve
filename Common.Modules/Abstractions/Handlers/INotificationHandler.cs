namespace Deve.Abstractions.Handlers;

public interface INotificationHandler;

public interface INotificationHandler<in TNotification> : INotificationHandler where TNotification : INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}
