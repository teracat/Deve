using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Username { get; init; }
    public UserActiveType? ActiveType { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
