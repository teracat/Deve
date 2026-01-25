namespace Deve.Identity.Users;

internal sealed class Core(IDataOptions options, IMediator mediator) : IUserData
{
    // Queries
    public async Task<ResultGetList<UserResponse>> GetAsync(UserGetListRequest? request, CancellationToken cancellationToken)
    {
        var query = request?.ToQuery() ?? new GetList.Query();
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(id);
        return await mediator.SendAsync(query, cancellationToken);
    }

    public async Task<ResultGet<UserResponse>> GetByUsernamePasswordAsync(UserGetByUsernamePasswordRequest request, CancellationToken cancellationToken)
    {
        var query = new GetByUsernamePassword.Query(request.Username, request.Password, request.ActiveType);
        return await mediator.SendAsync(query, cancellationToken);
    }

    // <hooks:core-queries>

    // Commands
    public async Task<ResultGet<ResponseId>> AddAsync(UserAddRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand();
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, UserUpdateRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = request.ToCommand(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> UpdatePasswordAsync(Guid id, UserUpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.MissingRequiredField);
        }

        var command = new UpdatePassword.Command(id, request.Password);
        return await mediator.SendAsync(command, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(id);
        return await mediator.SendAsync(command, cancellationToken);
    }

    // <hooks:core-commands>
}
