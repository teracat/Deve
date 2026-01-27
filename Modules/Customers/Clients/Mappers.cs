namespace Deve.Customers.Clients;

internal static class Mappers
{
    public static GetList.Query ToQuery(this ClientGetListRequest? request) => new()
    {
        Id = request?.Id,
        Name = request?.Name,
        TradeName = request?.TradeName,
        TaxId = request?.TaxId,
        TaxName = request?.TaxName,
        CityId = request?.CityId,
        StatusFilterType = request?.StatusFilterType,
        Offset = request?.Offset,
        Limit = request?.Limit,
        OrderBy = request?.OrderBy
    };

    public static Add.Command ToCommand(this ClientAddRequest request) => new()
    {
        Name = request.Name,
        TradeName = request.TradeName,
        TaxId = request.TaxId,
        TaxName = request.TaxName,
        CityId = request.CityId,
        Status = request.Status,
        Balance = request.Balance
    };

    public static Update.Command ToCommand(this ClientUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name,
        CityId = request.CityId
    };

    public static UpdateStatus.Command ToCommand(this ClientUpdateStatusRequest request, Guid id) => new(id, request.Status);

    // <hooks:mappers>
}
