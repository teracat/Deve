namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public sealed record ITEM_NAME_SINGULARGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
