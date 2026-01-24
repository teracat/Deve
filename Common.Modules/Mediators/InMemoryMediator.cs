using System.Reflection;
using Deve.Abstractions.Handlers;
using Deve.Logging;

namespace Deve.Mediators;

internal sealed class InMemoryMediator(IServiceProvider serviceProvider) : IMediator
{
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken) where TResponse : IResult
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var responseType = typeof(TResponse);
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Hander not registered for {requestType.Name} → {responseType.Name}");

        Task<TResponse> HandlerCall(CancellationToken ct)
        {
            var method = handlerType.GetMethod(nameof(IRequestHandler<,>.HandleAsync))!;
            return (Task<TResponse>)method.Invoke(handler, new object[] { request, ct })!;
        }

        var behaviorOpenType = typeof(IPipelineBehavior<,>);
        var behaviorClosedType = behaviorOpenType.MakeGenericType(requestType, responseType);
        var enumerableClosed = typeof(IEnumerable<>).MakeGenericType(behaviorClosedType);

        var resolved = serviceProvider.GetService(enumerableClosed);
        var behaviors = resolved is IEnumerable seq
            ? seq.Cast<object>().Reverse().ToArray()
            : Array.Empty<object>();

        RequestHandlerCallback<TResponse> next = HandlerCall;

        foreach (var behavior in behaviors)
        {
            var b = behavior;
            var method = behaviorClosedType.GetMethod(nameof(IPipelineBehavior<,>.HandleAsync))!;
            var prev = next;
            next = (cancellationToken) => (Task<TResponse>)method.Invoke(b, new object[] { request, prev, cancellationToken })!;
        }

        return next(cancellationToken);
    }

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
