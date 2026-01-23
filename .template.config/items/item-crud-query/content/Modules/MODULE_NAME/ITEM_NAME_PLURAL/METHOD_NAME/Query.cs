namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed record Query(Guid? Id) : IRequest<ResultGet<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>;
