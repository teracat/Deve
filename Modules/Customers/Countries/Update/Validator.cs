namespace Deve.Customers.Countries.Update;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(command?.Id))
                                             .CheckCountryFields(command?.Name, command?.IsoCode);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
