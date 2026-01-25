namespace Deve.Customers.Cities;

internal static class Mappers
{
    public static GetList.Query ToQuery(this CityGetListRequest? request) => new()
    {
        Id = request?.Id,
        Name = request?.Name,
        StateId = request?.StateId,
        Offset = request?.Offset,
        Limit = request?.Limit,
        OrderBy = request?.OrderBy
    };

    public static Add.Command ToCommand(this CityAddRequest request) => new()
    {
        Name = request.Name,
        StateId = request.StateId
    };

    public static Update.Command ToCommand(this CityUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name,
        StateId = request.StateId
    };

    // <hooks:mappers>
}
