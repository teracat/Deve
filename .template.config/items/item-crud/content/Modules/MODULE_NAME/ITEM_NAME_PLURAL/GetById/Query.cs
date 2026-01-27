namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<ITEM_NAME_SINGULARResponse>>;
