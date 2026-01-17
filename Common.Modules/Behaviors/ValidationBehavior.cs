namespace Deve.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResult<TResponse>
{
    public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerCallback<TResponse> nextStep, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nextStep);

        IResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(request)));

        var failure = validationResults.FirstOrDefault(result => !result.Success);
        if (failure?.Errors is not null)
        {
            return TResponse.Fail(failure.Errors);
        }

        return await nextStep(cancellationToken);
    }
}
