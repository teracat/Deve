namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal static class Mappers
{
    public static FEATURE_SINGULARResponse ToResponse(this FEATURE_SINGULAR obj) => new()
    {
        Id = obj.Id,
        Name = obj.Name
    };

    public static GetList.Query ToQuery(this FEATURE_SINGULARGetListRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Offset = request.Offset,
        Limit = request.Limit,
        OrderBy = request.OrderBy
    };

    public static Add.Command ToCommand(this FEATURE_SINGULARAddRequest request) => new()
    {
        Name = request.Name
    };

    public static Update.Command ToCommand(this FEATURE_SINGULARUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name
    };

    // <hooks:mappers>
}
