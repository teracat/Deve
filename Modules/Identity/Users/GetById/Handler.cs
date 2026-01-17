namespace Deve.Identity.Users.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<User> repositoryUser,
    IUserIdentityService identityService) : IGetQueryHandler<Query, UserResponse>
{
    public Task<ResultGet<UserResponse>> HandleAsync(Query request, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            if (!identityService.IsAuthenticated || identityService.UserIdentity is null)
            {
                return Result.FailGet<UserResponse>(options.LangCode, ResultErrorType.Unauthorized);
            }

            var user = repositoryUser.GetAsQueryable()
                                     .Where(x => x.Id == request.Id)
                                     .Select(x => x.ToResponse())
                                     .FirstOrDefault();
            if (user is null)
            {
                return Result.FailGet<UserResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            // Only admin or the user himself can get the user info
            if (!identityService.IsAdmin && user.Id != identityService.UserIdentity.Id)
            {
                return Result.FailGet<UserResponse>(options.LangCode, ResultErrorType.Unauthorized);
            }

            return Result.OkGet(user);
        }, cancellationToken);
}
