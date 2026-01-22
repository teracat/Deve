namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

internal static class Mappers
{
    public static ITEM_NAME_SINGULARResponse ToResponse(this ITEM_NAME_SINGULAR obj) => new(obj.Id, obj.Name);

    public static GetList.Query ToQuery(this ITEM_NAME_SINGULARGetListRequest request) => new(request.Id, request.Name, request.Offset, request.Limit, request.OrderBy);

    public static Add.Command ToCommand(this ITEM_NAME_SINGULARAddRequest request) => new(request.Name);

    public static Update.Command ToCommand(this ITEM_NAME_SINGULARUpdateRequest request, Guid id) => new(id, request.Name);

    // <hooks:mappers>
}
