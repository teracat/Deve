namespace Deve.Customers.States.Add;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                              .CheckStateFields(command?.Name, command?.CountryId);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
