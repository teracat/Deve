namespace Deve.Identity.Users.Update;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            if (command is null)
            {
                return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
            }

            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(command.Id))
                                             .CheckUserFields(command.Name, command.Username);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
