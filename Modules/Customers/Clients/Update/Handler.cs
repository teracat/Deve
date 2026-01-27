using Deve.Customers.Clients.Notifications;

namespace Deve.Customers.Clients.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Client> repository,
    IMediator mediator) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new Client()
        {
            Id = command.Id,
            Name = command.Name,
            CityId = command.CityId
        };

        if (!await repository.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        await mediator.PublishAsync(new ClientUpdated(command.Id));

        return Result.Ok();
    }
}
