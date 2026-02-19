using System.Reflection;
using Deve.Abstractions.Handlers;
using Deve.Abstractions.Publishers;
using Deve.Logging;

namespace Deve.Publishers;

internal sealed class InMemoryPublisher(IServiceProvider serviceProvider) : IPublisher
{
    public Task PublishAsync(INotification notification) =>
        Task.Run(() =>
        {
            if (notification is null)
            {
                return;
            }

            var notificationType = notification.GetType();

            var handlerOpenType = typeof(INotificationHandler<>);
            var closedHandlerType = handlerOpenType.MakeGenericType(notificationType);
            var enumerableClosed = typeof(IEnumerable<>).MakeGenericType(closedHandlerType);

            var resolved = serviceProvider.GetService(enumerableClosed);
            if (resolved is not IEnumerable seq)
            {
                return;
            }

            var handlers = seq.Cast<object>().ToArray();
            if (handlers.Length == 0)
            {
                return;
            }

            var handleMethod = closedHandlerType.GetMethod(
                nameof(INotificationHandler<>.HandleAsync),
                BindingFlags.Public | BindingFlags.Instance
            )!;

            foreach (var handler in handlers)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await InvokeHandlerAsync(handler, handleMethod, notification, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"Error executing handler {handler.GetType().FullName} for {notificationType.FullName}");
                    }
                });
            }

            static Task InvokeHandlerAsync(object handler, MethodInfo handleMethod, object notification, CancellationToken ct)
            {
                return (Task)handleMethod.Invoke(handler, new object[] { notification, ct })!;
            }
        });
}
