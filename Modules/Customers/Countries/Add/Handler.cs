namespace Deve.Customers.Countries.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Country> repositoryCountry) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var entity = new Country()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            IsoCode = command.IsoCode,
        };

        var newId = await repositoryCountry.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
