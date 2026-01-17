namespace Deve.Mediators;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : IResult;

    Task PublishAsync(INotification notification);
}
