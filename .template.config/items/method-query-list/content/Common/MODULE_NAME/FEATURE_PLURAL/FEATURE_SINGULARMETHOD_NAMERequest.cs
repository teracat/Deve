namespace Deve.MODULE_NAME.FEATURE_PLURAL;

public sealed record FEATURE_SINGULARMETHOD_NAMERequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
