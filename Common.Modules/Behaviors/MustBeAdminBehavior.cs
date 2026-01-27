namespace Deve.Behaviors;

public class MustBeAdminBehavior<TRequest, TResponse>(
    IDataOptions Options,
    IUserIdentityService IdentityService) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResult<TResponse>
{
    public virtual async Task<TResponse> HandleAsync(TRequest request, RequestHandlerCallback<TResponse> nextStep, CancellationToken cancellationToken)
    {
        if (!IdentityService.IsAdmin)
        {
            return TResponse.Fail(Options.LangCode, ResultErrorType.Unauthorized, null);
        }

        ArgumentNullException.ThrowIfNull(nextStep);

        return await nextStep(cancellationToken);
    }
}
