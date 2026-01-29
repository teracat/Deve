namespace Deve.Identity.Users.GetList;

internal sealed record Query : IRequest<ResultGetList<UserResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Username { get; init; }
    public string? PasswordHash { get; init; }
    public UserActiveType? UserActiveType { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
