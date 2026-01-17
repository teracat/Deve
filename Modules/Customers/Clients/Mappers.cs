using Deve.Customers.Clients.GetList;
using Deve.Customers.Clients.UpdateStatus;

namespace Deve.Customers.Clients;

internal static class Mappers
{
    public static Query ToQuery(this ClientGetListRequest? request) => new(request?.Id, request?.Name, request?.TradeName, request?.TaxId, request?.TaxName, request?.CityId, request?.StatusFilterType, request?.Offset, request?.Limit, request?.OrderBy);

    public static Add.Command ToCommand(this ClientAddRequest request) => new(request.Name, request.TradeName, request.TaxId, request.TaxName, request.CityId, request.Status, request.Balance);

    public static Update.Command ToCommand(this ClientUpdateRequest request, Guid id) => new(id, request.Name, request.CityId);

    public static UpdateClientStatusCommand ToCommand(this ClientUpdateStatusRequest request, Guid id) => new(id, request.Status);
}
