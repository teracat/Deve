namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal sealed class Core(IDataOptions options, IMediator mediator) : IFEATURE_SINGULARData
{
    // Queries
    public async Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync(FEATURE_SINGULARGetListRequest? request, CancellationToken cancellationToken)
    {
        var query = request?.ToQuery() ?? new GetList.Query();
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<FEATURE_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // <hooks:core-queries>

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(FEATURE_SINGULARAddRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, FEATURE_SINGULARUpdateRequest request, CancellationToken cancellationToken)
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
