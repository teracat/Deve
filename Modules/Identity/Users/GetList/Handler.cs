namespace Deve.Identity.Users.GetList;

internal sealed class Handler(
    IRepository<User> repositoryUser) : IGetListQueryHandler<Query, UserResponse>
{
    public Task<ResultGetList<UserResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryUser.GetAsQueryable();

            // Apply filters
            if (request.Id.HasValue)
            {
                query = query.Where(x => x.Id == request.Id.Value);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(x => x.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.PasswordHash))
            {
                query = query.Where(x => x.PasswordHash.Equals(request.PasswordHash, StringComparison.Ordinal));
            }
            if (request.UserActiveType.HasValue)
            {
                switch (request.UserActiveType.Value)
                {
                    case UserActiveType.OnlyActive:
                        query = query.Where(x => x.Status == UserStatus.Active);
                        break;
                    case UserActiveType.OnlyInactive:
                        query = query.Where(x => x.Status == UserStatus.Inactive);
                        break;
                    case UserActiveType.All:
                        // No filter
                        break;
                    default:
                        break;
                }
            }

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(UserGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.Id),
                "EMAIL" => query.OrderBy(x => x.Email),
                "USERNAME" => query.OrderBy(x => x.Username),
                "BIRTHDAY" => query.OrderBy(x => x.Birthday),
                "JOINED" => query.OrderBy(x => x.Joined),
                "STATUS" => query.OrderBy(x => x.Status),
                _ => query.OrderBy(x => x.Name),
            };

            // Apply pagination
            var offset = request?.Offset ?? 0;
            var limit = request?.Limit ?? Constants.DefaultLimit;
            var list = query.Select(x => x.ToResponse())
                            .Skip(offset)
                            .Take(limit)
                            .ToList();

            return Result.OkGetList(list, offset, limit, orderBy, totalCount);
        }, cancellationToken);
}
