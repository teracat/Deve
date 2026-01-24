namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

internal sealed class Core(IDataOptions options, IMediator mediator) : IITEM_NAME_SINGULARData
{
    // Queries
    public async Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(ITEM_NAME_SINGULARGetListRequest? request, CancellationToken cancellationToken)
    {
        var query = request?.ToQuery() ?? new GetList.Query(null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // <hooks:core-queries>

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(ITEM_NAME_SINGULARAddRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, ITEM_NAME_SINGULARUpdateRequest request, CancellationToken cancellationToken)
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
