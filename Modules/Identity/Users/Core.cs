namespace Deve.Identity.Users;

internal sealed class Core(IDataOptions options, IMediator mediator) : IUserData
{
    // Queries
    public async Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request, CancellationToken cancellationToken = default)
    {
        var query = request?.ToQuery() ?? new GetList.Query(null, null, null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGetList<UserResponse>> GetAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetList.Query(null, null, null, null, null, null, null, null);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var query = new GetByUsernamePassword.Query(request.Username, request.Password, request.ActiveType);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, UserUpdateRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = new UpdatePassword.Command(id, request.Password);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new Delete.Command(id);
        return await mediator.SendAsync(command, cancellationToken);
    }
}
