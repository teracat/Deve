namespace Deve.Customers.Clients;

internal sealed class Core(IDataOptions options, IMediator mediator) : IClientData
{
    // Queries
    public async Task<ResultGetList<ClientListResponse>> GetAsync(ClientGetListRequest? request, CancellationToken cancellationToken)
    {
        var query = request?.ToQuery() ?? new GetList.Query(null, null, null, null, null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<ClientResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // <hooks:core-queries>

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(ClientAddRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateStatusAsync(Guid id, ClientUpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    // <hooks:core-commands>
}
