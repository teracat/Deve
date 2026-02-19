using Deve.Customers.Clients.Notifications;

namespace Deve.Customers.Clients.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Client> repositoryClient,
    IPublisher publisher) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        if (!await repositoryClient.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        await publisher.PublishAsync(new ClientDeleted(command.Id));

        return Result.Ok();
    }
}
