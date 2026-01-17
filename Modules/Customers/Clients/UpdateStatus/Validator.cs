namespace Deve.Customers.Clients.UpdateStatus;

internal sealed class Validator(IDataOptions options) : IValidator<UpdateClientStatusCommand>
{
    public Task<Result> ValidateAsync(UpdateClientStatusCommand command, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            if (command is null)
            {
                return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
            }

            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(command.Id));
            return resultBuilder.ToResult();
        }, cancellationToken);
}
