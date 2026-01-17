namespace Deve.Customers.Clients.Add;

internal sealed class Validator(IDataOptions options) : IValidator<Command>
{
    public Task<Result> ValidateAsync(Command command, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var resultBuilder = ResultBuilder.Create(options.LangCode)
                                             .CheckClientFields(command?.Name, command?.CityId);
            return resultBuilder.ToResult();
        }, cancellationToken);
}
