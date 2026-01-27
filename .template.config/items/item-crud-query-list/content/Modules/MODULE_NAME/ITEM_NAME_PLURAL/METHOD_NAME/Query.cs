namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed record Query : IRequest<ResultGetList<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
