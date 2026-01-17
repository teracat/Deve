namespace Deve.Customers.Cities.Add;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckCityFields(command?.Name, command?.StateId);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
