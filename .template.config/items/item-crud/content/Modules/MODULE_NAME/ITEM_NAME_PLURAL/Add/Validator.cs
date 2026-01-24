namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

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
                                             .CheckITEM_NAME_SINGULARFields(command.Name);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
