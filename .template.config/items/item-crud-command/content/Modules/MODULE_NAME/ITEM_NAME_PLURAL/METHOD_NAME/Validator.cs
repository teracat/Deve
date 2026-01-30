namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

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
                                             .CheckNotNullOrEmpty(new Field(command.Id), new Field(command.Name));
            return resultBuilder.ToResult();
        }, cancellationToken);
}
