namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

internal sealed record Command(string Name) : IRequest<ResultGet<ResponseId>>;
