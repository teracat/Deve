namespace Deve.Customers.Cities.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<City> repositoryCity) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new City()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            StateId = command.StateId
        };

        var newId = await repositoryCity.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
