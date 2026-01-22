using Deve.Identity.Users.GetList;

namespace Deve.Identity.Users;

internal static class Mappers
{
    public static UserResponse ToResponse(this User user) => new(user.Id, user.Name, user.Username, user.Status, user.Role, user.Email, user.Birthday, user.Joined);

    public static Query ToQuery(this UserGetListRequest request) => new(request.Id, request.Name, request.Username, null, request.ActiveType, request.Offset, request.Limit, request.OrderBy);

    public static Add.Command ToCommand(this UserAddRequest request) => new(request.Name, request.Username, request.Password, request.Status, request.Role, request.Email, request.Birthday);

    public static Update.Command ToCommand(this UserUpdateRequest request, Guid id) => new(id, request.Name, request.Username, request.Status, request.Role, request.Email, request.Birthday);

    // <hooks:mappers>
}
