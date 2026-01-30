namespace Deve.Customers.Clients.Update;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(command?.Id))
                                             .CheckClientFields(command?.Name, command?.CityId);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
