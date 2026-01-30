namespace Deve.Mediators;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) where TResponse : IResult => SendAsync(request, CancellationToken.None);
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken) where TResponse : IResult;

    Task PublishAsync(INotification notification);
}
