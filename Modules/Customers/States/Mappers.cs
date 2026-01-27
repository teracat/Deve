namespace Deve.Customers.States;

internal static class Mappers
{
    public static GetList.Query ToQuery(this StateGetListRequest? request) => new()
    {
        Id = request?.Id,
        Name = request?.Name,
        CountryId = request?.CountryId,
        Offset = request?.Offset,
        Limit = request?.Limit,
        OrderBy = request?.OrderBy
    };

    public static Add.Command ToCommand(this StateAddRequest request) => new()
    {
        Name = request.Name,
        CountryId = request.CountryId
    };

    public static Update.Command ToCommand(this StateUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name,
        CountryId = request.CountryId
    };

    // <hooks:mappers>
}
