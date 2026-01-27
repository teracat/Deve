namespace Deve.Behaviors;

public class MustBeAuthenticatedBehavior<TRequest, TResponse>(
    IDataOptions Options,
    IUserIdentityService IdentityService) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull where TResponse : IResult<TResponse>
{
    public virtual async Task<TResponse> HandleAsync(TRequest request, RequestHandlerCallback<TResponse> nextStep, CancellationToken cancellationToken)
    {
        if (!IdentityService.IsAuthenticated)
        {
            return TResponse.Fail(Options.LangCode, ResultErrorType.Unauthorized, null);
        }

        ArgumentNullException.ThrowIfNull(nextStep);

        return await nextStep(cancellationToken);
    }
}
