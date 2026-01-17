using Deve.Customers.States.GetList;

namespace Deve.Customers.States;

internal static class Mappers
{
    public static Query ToQuery(this StateGetListRequest? request) => new(request?.Id, request?.Name, request?.CountryId, request?.Offset, request?.Limit, request?.OrderBy);

    public static Add.Command ToCommand(this StateAddRequest request) => new(request.Name, request.CountryId);

    public static Update.Command ToCommand(this StateUpdateRequest request, Guid id) => new(id, request.Name, request.CountryId);
}
