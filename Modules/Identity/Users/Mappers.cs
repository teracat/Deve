namespace Deve.Identity.Users;

internal static class Mappers
{
    public static UserResponse ToResponse(this User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Username = user.Username,
        Status = user.Status,
        Role = user.Role,
        Email = user.Email,
        Birthday = user.Birthday,
        Joined = user.Joined
    };

    public static GetList.Query ToQuery(this UserGetListRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Username = request.Username,
        UserActiveType = request.ActiveType,
        Offset = request.Offset,
        Limit = request.Limit,
        OrderBy = request.OrderBy
    };

    public static Add.Command ToCommand(this UserAddRequest request) => new()
    {
        Name = request.Name,
        Username = request.Username,
        Password = request.Password,
        Status = request.Status,
        Role = request.Role,
        Email = request.Email,
        Birthday = request.Birthday
    };

    public static Update.Command ToCommand(this UserUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name,
        Username = request.Username,
        Status = request.Status,
        Role = request.Role,
        Email = request.Email,
        Birthday = request.Birthday
    };

    // <hooks:mappers>
}
