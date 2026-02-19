using Deve.Customers.Clients.Notifications;

namespace Deve.Customers.Clients.UpdateStatus;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Client> repository,
    IPublisher publisher) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repository.GetAsQueryable()
                               .FirstOrDefault(x => x.Id == command.Id);

        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.InvalidId);
        }

        entity.Status = command.Status;

        if (!await repository.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        await publisher.PublishAsync(new ClientUpdated(command.Id));

        return Result.Ok();
    }
}
