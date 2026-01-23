namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed record Query(Guid? Id, string? Name, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>;
