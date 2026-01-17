namespace Deve.Auth.RefreshToken;

internal sealed class Validator(IDataOptions options) : IValidator<RefreshTokenRequest>
{
    public Task<Result> ValidateAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            if (request is null)
            {
                return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
            }

            return ResultBuilder.Create(options.LangCode)
                                .CheckNotNullOrEmpty(new Field(request.Token))
                                .ToResult();
        }, cancellationToken);
}
