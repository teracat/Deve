namespace Deve.Customers.Clients.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Client> repository) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var entity = new Client()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            TradeName = command.TradeName,
            TaxId = command.TaxId,
            TaxName = command.TaxName,
            CityId = command.CityId,
            Status = command.Status,
            Balance = command.Balance,
        };

        var newId = await repository.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
