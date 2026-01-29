namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed record Query : IRequest<ResultGet<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>
{
    public required Guid Id { get; init; }
}
