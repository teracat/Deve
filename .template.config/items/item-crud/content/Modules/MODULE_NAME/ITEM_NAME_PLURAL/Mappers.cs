namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

internal static class Mappers
{
    public static ITEM_NAME_SINGULARResponse ToResponse(this ITEM_NAME_SINGULAR obj) => new()
    {
        Id = obj.Id,
        Name = obj.Name
    };

    public static GetList.Query ToQuery(this ITEM_NAME_SINGULARGetListRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Offset = request.Offset,
        Limit = request.Limit,
        OrderBy = request.OrderBy
    };

    public static Add.Command ToCommand(this ITEM_NAME_SINGULARAddRequest request) => new()
    {
        Name = request.Name
    };

    public static Update.Command ToCommand(this ITEM_NAME_SINGULARUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name
    };

    // <hooks:mappers>
}
