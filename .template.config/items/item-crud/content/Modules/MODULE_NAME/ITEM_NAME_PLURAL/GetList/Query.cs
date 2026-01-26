namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetList;

internal sealed record Query : IRequest<ResultGetList<ITEM_NAME_SINGULARResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
