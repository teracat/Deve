namespace Deve.Handlers;

public interface IRequestHandler;

public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler, IRequest<TResponse> where TResponse : IResult
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
