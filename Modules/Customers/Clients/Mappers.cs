namespace Deve.Customers.Clients;

internal static class Mappers
{
    public static GetList.Query ToQuery(this ClientGetListRequest? request) => new(request?.Id, request?.Name, request?.TradeName, request?.TaxId, request?.TaxName, request?.CityId, request?.StatusFilterType, request?.Offset, request?.Limit, request?.OrderBy);

    public static Add.Command ToCommand(this ClientAddRequest request) => new(request.Name, request.TradeName, request.TaxId, request.TaxName, request.CityId, request.Status, request.Balance);

    public static Update.Command ToCommand(this ClientUpdateRequest request, Guid id) => new(id, request.Name, request.CityId);

    public static UpdateStatus.Command ToCommand(this ClientUpdateStatusRequest request, Guid id) => new(id, request.Status);

    // <hooks:mappers>
}
