namespace Deve.Customers.Cities.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepository<City> repositoryCity) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new City()
        {
            Id = command.Id,
            Name = command.Name.Trim(),
            StateId = command.StateId
        };

        if (!await repositoryCity.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
