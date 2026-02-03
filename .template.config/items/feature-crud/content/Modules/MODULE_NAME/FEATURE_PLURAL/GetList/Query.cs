namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetList;

internal sealed record Query : IRequest<ResultGetList<FEATURE_SINGULARResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
