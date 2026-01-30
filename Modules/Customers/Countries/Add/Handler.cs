namespace Deve.Customers.Countries.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Country> repositoryCountry) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new Country()
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            IsoCode = command.IsoCode.Trim(),
        };

        var newId = await repositoryCountry.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
