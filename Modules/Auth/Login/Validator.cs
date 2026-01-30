namespace Deve.Auth.Login;

internal sealed class Validator(IDataOptions options) : IValidator<LoginRequest>
{
    public Task<Result> ValidateAsync(LoginRequest request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            if (request is null)
            {
                return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
            }

            return ResultBuilder.Create(options.LangCode)
                                .CheckNotNullOrEmpty(new Field(request.Username),
                                                     new Field(request.Password))
                                .ToResult();
        }, cancellationToken);
}
