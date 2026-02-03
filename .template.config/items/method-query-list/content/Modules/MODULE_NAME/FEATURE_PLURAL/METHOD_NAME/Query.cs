namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed record Query : IRequest<ResultGetList<FEATURE_SINGULARMETHOD_NAMEResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
