namespace Deve.Customers.States;

internal sealed class Core(IDataOptions options, IMediator mediator) : IStateData
{
    // Queries
    public async Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request, CancellationToken cancellationToken)
    {
        var query = request?.ToQuery() ?? new GetList.Query(null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<StateResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // <hooks:core-queries>

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, StateUpdateRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

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
