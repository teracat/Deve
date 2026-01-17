using Deve.Customers.Cities.GetList;

namespace Deve.Customers.Cities;

internal static class Mappers
{
    public static Query ToQuery(this CityGetListRequest? request) => new(request?.Id, request?.Name, request?.StateId, request?.Offset, request?.Limit, request?.OrderBy);

    public static Add.Command ToCommand(this CityAddRequest request) => new(request.Name, request.StateId);

    public static Update.Command ToCommand(this CityUpdateRequest request, Guid id) => new(id, request.Name, request.StateId);
}
