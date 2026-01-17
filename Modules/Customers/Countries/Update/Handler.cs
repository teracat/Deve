namespace Deve.Customers.Countries.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Country> repositoryCountry) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var entity = new Country()
        {
            Id = command.Id,
            Name = command.Name,
            IsoCode = command.IsoCode,
        };

        if (!await repositoryCountry.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
