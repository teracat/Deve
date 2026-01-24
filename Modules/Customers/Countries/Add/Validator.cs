namespace Deve.Customers.Countries.Add;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckCountryFields(command?.Name, command?.IsoCode);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
