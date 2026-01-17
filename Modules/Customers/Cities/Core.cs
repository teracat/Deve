namespace Deve.Customers.Cities;

internal sealed class Core(IDataOptions options, IMediator mediator) : ICityData
{
    // Queries
    public async Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request, CancellationToken cancellationToken = default)
    {
        var query = request?.ToQuery() ?? new GetList.Query(null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGetList<CityResponse>> GetAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetList.Query(null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<CityResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, CityUpdateRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new Delete.Command(id);
        return await mediator.SendAsync(command, cancellationToken);
    }
}
