namespace Deve.Mediators;

public delegate Task<TResponse> RequestHandlerCallback<TResponse>(CancellationToken cancellationToken = default);

public interface IPipelineBehavior;

public interface IPipelineBehavior<in TRequest, TResponse> : IPipelineBehavior where TRequest : notnull
{
    Task<TResponse> HandleAsync(TRequest request, RequestHandlerCallback<TResponse> nextStep, CancellationToken cancellationToken);
}
