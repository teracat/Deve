namespace Deve.Customers.Cities.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<City> repositoryCity,
    IRepository<Client> repositoryClient) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var hasClients = repositoryClient.GetAsQueryable()
                                         .Any(x => x.CityId == command.Id);
        if (hasClients)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotAllowed);
        }

        if (!await repositoryCity.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
