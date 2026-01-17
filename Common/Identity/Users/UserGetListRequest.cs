using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserGetListRequest(Guid? Id, string? Name, string? Username, UserActiveType? ActiveType, int? Offset, int? Limit, string? OrderBy)
{
    public static UserGetListRequest Create(Guid? id = null, string? name = null, string? username = null, UserActiveType? activeType = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            Username: username,
            ActiveType: activeType,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
